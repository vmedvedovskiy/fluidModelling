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
            

            var coordFuncs = new CoordinateFunctions().Construct(Common.Instance.M1, Common.Instance.M2);
            // Compile to IL code using the variables given.

            var _jacobian = new Jacobian();
            IList<CompiledFunction> cfs = new List<CompiledFunction>();
            IList<double> vals = new List<Double>();
            foreach (var cat in coordFuncs)
            {
                var j = new Jacobian();
                var composition = cat.Compose(j);
                vals.Add(Integration.Integrate(composition));
                cfs.Add(Compiler.Compile(composition.Expression, composition.R, composition.Th));
            }

            vals = vals.Distinct<double>().ToList();
        }

        #endregion

        private void ReportProgress()
        {
            this.Progress++;
            this.Worker.ReportProgress(this.Progress);
        }
    }

    internal class Jacobian : VariabledFunction
    {
        public override Function GetExpression(Variable r, Variable th)
        {
            var a = Common.Instance.A;
            var b = Common.Instance.B;
            return a * b * r * Function.Pow(Function.Cos(th), 2)
                        + a * b * r * Function.Pow(Function.Sin(th), 2);
        }
    }

}
