namespace Diploma.Managed
{
    using Diploma.Functions;
    using FuncLib.Functions;
    using FuncLib.Functions.Compilation;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class IterationCompletedArgs
    {
        public double[] Alphas { get; private set; }

        public int Number { get; private set; }

        public IterationCompletedArgs(double[] alphas, int number)
        {
            this.Alphas = alphas;
            this.Number = number;
        }
    }

    public class Nonlinear
    {

        #region Fields
        private double eps = Math.Pow(10, -6);
        private int iterationProgress;
        private int iterationsCount;
        private int actionsCount; 

        public delegate void IterationCompletedHandler(object sender, IterationCompletedArgs e);
        public event IterationCompletedHandler IterationCompleted;

        #endregion

        #region Properties

        public BackgroundWorker Worker { get; set; }

        #endregion

        #region Constructors

        public Nonlinear()
        {
            this.Worker = new BackgroundWorker();
            this.Worker.WorkerReportsProgress = true;
            this.Worker.WorkerSupportsCancellation = true;
            this.Prepare();
        }

        #endregion

        #region Methods

        protected void Prepare()
        {
            Variable r = new Variable();
            Variable th = new Variable();

            var f = new ProjectionFunction().Construct(r, th);
            var phi = new CoordinateFunctions().Construct(r, th);
            var count = phi.Count;
            var u = new U(count);
            double[,] lp = null;
            this.Worker.DoWork += (sender, e) =>
            {
                while (this.iterationsCount < 20)
                {
                    if (lp == null)
                    {
                        this.actionsCount = count * count + count;
                        lp = this.GetLeftPartAsync(count, f, phi, r, th);
                    }

                    DoIteration(lp, r, th, f, count, u);
                }
            };
        }

        private void DoIteration(double[,] lp, Variable r, Variable th, IList<Function> f, int count, U u)
        {
            this.actionsCount = count;

            var uf = u.GetExpression(r, th);
            var rp = this.GetRightPartAsync(count, f, uf, r, th);

            int info;
            double[] alphas;
            alglib.densesolverreport report;
            alglib.rmatrixsolve(lp, count, rp, out info, out report, out alphas);

            this.OnIterationCompleted(alphas, this.iterationsCount);
        }

        #endregion

        private double[] GetRightPartAsync(int count, IList<Function> f, Function u, Variable r, Variable th)
        {
            var result = new double[count];
            var fo = new FOperator();
            var j = new Jacobian().GetExpression(r, th);
            var fApplied = fo.Apply(u, r, th);
            for (var i = 0; i < count; ++i)
            {
                this.ReportProgress();
                result[i] = Integration.Integrate(Compiler.Compile(f[i] * fApplied, r, th));
            }

            return result;

        }

        private double[,] GetLeftPartAsync(int count, IList<Function> f, IList<Function> phi, Variable r, Variable th)
        {
            var result = new double[count, count];
            var e2 = new E2();
            var jac = new Jacobian().GetExpression(r, th);
            for (var i = 0; i < count; ++i)
            {
                var fe = f[i];
                for (var j = 0; j < count; ++j)
                {
                    this.ReportProgress();
                    var e2phi = e2.Apply(phi[j], r, th);
                    var @int = Integration.Integrate(Compiler.Compile(e2phi * fe, r, th));
                    result[i, j] = @int;
                }
            }

            return result;

        }

        private void ReportProgress()
        {
            this.Worker.ReportProgress(100 * this.iterationProgress++ / this.actionsCount);
        }

        private void OnIterationCompleted(double[] alphas, int i)
        {
            this.iterationProgress = 0;
            if (IterationCompleted != null)
            {
                try
                {
                    IterationCompleted(this, new IterationCompletedArgs(alphas, i));
                }
                catch (Exception e)
                {

                }
            }

            this.iterationsCount++;
        }
    }
}
