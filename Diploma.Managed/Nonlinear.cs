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
            var projectionFuncs = new ProjectionFunctions().Construct(Common.Instance.M1, Common.Instance.M2);

        }

        #endregion

        private void ReportProgress()
        {
            this.Progress++;
            this.Worker.ReportProgress(this.Progress);
        }
    }
}
