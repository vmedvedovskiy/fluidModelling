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
            

            var f = new ProjectionFunction().Construct(Common.Instance.M1, Common.Instance.M2);
            var phi = new CoordinateFunctions().Construct(Common.Instance.M1, Common.Instance.M2);
            var count = Common.Instance.M1 + Common.Instance.M2;
            var u = new U(count);
            // Compile to IL code using the variables given.

            IList<double> vals = new List<Double>();

            var B = new B();
            var j = new Jacobian();

            vals = vals.Distinct<double>().ToList();
            var rp = this.GetRightPartAsync(count, f, u);
            var lp = this.GetLeftPartAsync(count, f, phi);
        }

        #endregion

        private void ReportProgress()
        {
            this.Progress++;
            this.Worker.ReportProgress(this.Progress);
        }

        private double[] GetRightPartAsync(int count, IList<IVariabledFunction> f, IVariabledFunction u)
        {
            var result = new double[count];
            var fo = new FOperator();
            var fApplied = fo.Apply(u.GetExpression(u.R, u.Th), u.R, u.Th);
            for (var i = 0; i < count; ++i)
            {
                var @int = Integration.Integrate(Compiler.Compile(f[i].GetExpression(u.R, u.Th) * fApplied, u.R, u.Th));
                result[i] = @int;
            }

            return result;

        }

        private double[, ] GetLeftPartAsync(int count, IList<IVariabledFunction> f, IList<IVariabledFunction> phi)
        {
            var result = new double[count, count];
            var e2 = new E2();
            for (var i = 0; i < count; ++i)
            {
                for (var j = 0; j < count; ++j)
                {
                    var e2phi = e2.Apply(phi[j].GetExpression(phi[j].R, phi[j].Th), phi[j].R, phi[j].Th);
                    var fe = f[i].GetExpression(phi[j].R, phi[j].Th);
                    var @int = Integration.Integrate(Compiler.Compile(e2phi * fe, phi[j].R, phi[j].Th));
                    result[i, j] = @int;
                }
            }

            return result;

        }
    }
}
