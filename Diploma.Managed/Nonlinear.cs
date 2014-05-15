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
            

            var phi = new CoordinateFunctions().Construct(Common.Instance.M1, Common.Instance.M2);
            var f = new CoordinateFunctions().Construct(Common.Instance.M1, Common.Instance.M2);
            var count = Common.Instance.M1 + Common.Instance.M2;
            var u = new U(count);
            // Compile to IL code using the variables given.

            IList<double> vals = new List<Double>();

            var B = new B();
            var j = new Jacobian();

            foreach (var cat in phi)
            {
                vals.Add(Integration.Integrate(Compiler.Compile(B.Apply(cat.GetExpression(cat.R, cat.Th), cat.R, cat.Th) * j.GetExpression(cat.R, cat.Th), cat.R, cat.Th)));
            }

            vals = vals.Distinct<double>().ToList();
            var rp = this.GetRightPartAsync(count, f, u);
            rp = rp.Distinct<double>().ToArray();
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
                var @int = Integration.Integrate(Compiler.Compile(f[i].GetExpression(u.R, u.Th) * fApplied));
                result[i] = @int;
            }

            return result;

        }
    }
}
