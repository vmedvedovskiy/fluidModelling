namespace Diploma.Managed
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Diploma.Functions;
    using FuncLib.Functions;
    using FuncLib.Functions.Compilation;

    public class Nonlinear
    {

        #region Fields
        private double eps = Math.Pow(10, -6);

        #endregion

        #region Properties

        public double[] Result { get; set; }
        public int ActionsCount { get; set; }
        public int Progress { get; set; }
        public BackgroundWorker Worker { get; set; }

        #endregion

        #region Constructors

        public Nonlinear()
        {
            this.Worker = new BackgroundWorker();
            this.Worker.WorkerReportsProgress = true;
            this.Worker.WorkerSupportsCancellation = true;
            this.Result = new double[0];
        }

        #endregion

        #region Methods

        public void Compute()
        {
            Variable r = new Variable();
            Variable th = new Variable();
            

            var f = new ProjectionFunction().Construct(r, th);
            var phi = new CoordinateFunctions().Construct(r, th);
            var count = phi.Count;
            var u = new U(count).GetExpression(r, th);
            // Compile to IL code using the variables given.

            // var e2 = new FOperator().Apply(new Omega().GetExpression(r, th), r, th).Value(r | 1, th | 1);

            var rp = this.GetRightPartAsync(count, f, u, r, th);
            var lp = this.GetLeftPartAsync(count, f, phi, r, th);

            int info;
            double[] alphas;
            alglib.densesolverreport report;
            alglib.rmatrixsolve(lp, count, rp, out info, out report, out alphas);
            report = report;
        }

        #endregion

        private void ReportProgress()
        {
            this.Progress++;
            this.Worker.ReportProgress(this.Progress);
        }

        private double[] GetRightPartAsync(int count, IList<Function> f, Function u, Variable r, Variable th)
        {
            var result = new double[count];
            var fo = new FOperator();
            var j = new Jacobian().GetExpression(r, th);
            var fApplied = fo.Apply(u, r, th);
            for (var i = 0; i < count; ++i)
            {
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
                    var e2phi = e2.Apply(phi[j], r, th);
                    var @int = Integration.Integrate(Compiler.Compile(e2phi * fe, r, th));
                    result[i, j] = @int;
                }
            }

            return result;

        }
    }
}
