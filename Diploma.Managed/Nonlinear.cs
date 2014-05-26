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

        public double Norm { get; private set; }

        public IterationCompletedArgs(double[] alphas, int number, double norm)
        {
            this.Alphas = alphas;
            this.Number = number;
            this.Norm = norm;
        }
    }

    public class ComputationCompletedArgs
    {
        public ComputationCompletedArgs()
        {
        }
    }

    public class Nonlinear
    {

        #region Fields
        private double eps = Math.Pow(10, -6);
        private int iterationProgress;
        private int iterationsCount;
        private int actionsCount;
        private const int MAX_ITERATIONS = 100;

        public delegate void IterationCompletedHandler(object sender, IterationCompletedArgs e);
        public event IterationCompletedHandler IterationCompleted;

        public delegate void ComputationCompletedHandler(object sender, ComputationCompletedArgs e);
        public event ComputationCompletedHandler ComputationCompleted;

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

        public void Prepare()
        {
            Variable r = new Variable();
            Variable th = new Variable();
            double[,] lp = null;

            this.Worker.DoWork += (sender, e) =>
            {
                var f = new ProjectionFunction().Construct(r, th);
                var phi = new CoordinateFunctions().Construct(r, th);
                var count = phi.Count;
                var u = new U(count);
                var Re = Common.Instance.Re;

                while (true && this.iterationsCount < MAX_ITERATIONS)
                {
                    // считаем линейную задачу для начального приближения
                    if (this.iterationsCount == 0)
                    {
                        Common.Instance.Re = 0;
                    }
                    else
                    {
                        Common.Instance.Re = Re;
                    }

                    if (lp == null)
                    {
                        this.actionsCount = count * count + count;
                        lp = this.GetLeftPartAsync(count, f, phi, r, th);
                    }

                    var norm = DoIteration(lp, r, th, f, count, u);
                    this.actionsCount = count;
                    if (norm <= this.eps && this.iterationsCount > 1)
                    {
                        break;
                    }
                }
            };
        }

        private double DoIteration(double[,] lp, Variable r, Variable th, IList<Function> f, int count, U u)
        {
            var uf = u.GetExpression(r, th);
            var rp = this.GetRightPartAsync(count, f, uf, r, th);

            int info;
            double[] alphas;
            alglib.densesolverreport report;
            alglib.rmatrixsolve(lp, count, rp, out info, out report, out alphas);
            var oldNorm = u.GetNorm();
            u.SetAlphas(alphas);
            var newNorm = u.GetNorm();
            var subtact = Math.Abs(newNorm - oldNorm);
            this.OnIterationCompleted(alphas, this.iterationsCount, subtact);
            return subtact;
        }

        #endregion

        private double[] GetRightPartAsync(int count, IList<Function> f, Function u, Variable r, Variable th)
        {
            var result = new double[count];
            var fo = new FOperator();
            for (var i = 0; i < count; ++i)
            {
                this.ReportProgress();
                result[i] = Integration.Integrate(Compiler.Compile(f[i] * fo.Apply(u, r, th), r, th));
            }

            return result;

        }

        private double[,] GetLeftPartAsync(int count, IList<Function> f, IList<Function> phi, Variable r, Variable th)
        {
            var result = new double[count, count];
            var e2 = new E2();
            for (var i = 0; i < count; ++i)
            {
                for (var j = 0; j < count; ++j)
                {
                    this.ReportProgress();
                    var e2phi = e2.Apply(phi[j], r, th);
                    var @int = Integration.Integrate(Compiler.Compile(e2phi * f[i], r, th));
                    result[i, j] = @int;
                }
            }

            return result;

        }

        private void ReportProgress()
        {
            this.Worker.ReportProgress(100 * this.iterationProgress++ / this.actionsCount);
        }

        private void OnIterationCompleted(double[] alphas, int i, double norm)
        {
            this.iterationProgress = 0;
            if (IterationCompleted != null)
            {
                IterationCompleted(this, new IterationCompletedArgs(alphas, i, norm));
            }

            this.iterationsCount++;
        }

        private void OnCompuationCompleted()
        {
            this.iterationProgress = 0;
            if (ComputationCompleted != null)
            {
                ComputationCompleted(this, new ComputationCompletedArgs());
            }

            this.iterationsCount++;
        }
    }
}
