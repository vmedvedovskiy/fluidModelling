
namespace Diploma.Managed
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.ComponentModel;
    using System.Threading;

    public class Linear
    {
        #region Fields

        private CoordFunction function;
        private CoordFunction currentI;
        private CoordFunction currentJ;
        private double eps = Math.Pow(10, -6);

        #endregion

        #region Properties

        public double[] Result { get; set; }
        public int ActionsCount { get; set; }
        public int Progress { get; set; }
        public BackgroundWorker Worker { get; set; }

        #endregion

        #region Constructors

        public Linear()
        {
            this.Worker = new BackgroundWorker();
            this.Worker.WorkerReportsProgress = true;
            this.Worker.WorkerSupportsCancellation = true;
            this.Result = new double[0];
        }

        #endregion

        #region Methods

        public void DoParallel()
        {
            List<CoordFunction> simpleFunctions = Functions.GetCoordFunctions();
            List<CoordFunction> functions = Functions.GetOperatorCoordFunctions();
            int count = functions.Count;

            this.ActionsCount = count * count + count + 1; // nxn интегралов-левая часть, n интегралов-правая часть, 1-решение СЛАУ

            this.Worker.DoWork += new DoWorkEventHandler((object sender, DoWorkEventArgs e) =>
            {
                var rhsDictionary = new Dictionary<int, double>();
                List<double> rhs = new List<double>(functions.Count);
                List<Task> tasks = new List<Task>();
                foreach (CoordFunction func in simpleFunctions)
                {
                    Task task = null;
                    task = Task.Factory.StartNew((function) =>
                    {
                        //минус-потому что потом переносим в правую часть системы 
                        rhsDictionary[rhsDictionary.Where(x => x.Value == task.Id).First().Key] = -Gauss.Integrate((double ro, double phi,
                            double a, double b, double m, double r, double uinf) =>
                        {
                            return Common.Instance.makeReplacement(ro, phi, GeneratedFunctions.f0) * Common.Instance.makeReplacement(ro, phi, (CoordFunction)function) * Common.Instance.jac(ro, phi);
                        });
                        this.ReportProgress();
                    }, func, CancellationToken.None, TaskCreationOptions.PreferFairness, TaskScheduler.Default);

                    tasks.Add(task);
                    rhsDictionary.Add(simpleFunctions.IndexOf(func), tasks.Last().Id);
                }

                Task.WaitAll(tasks.ToArray());

                foreach (var kvp in rhsDictionary)
                {
                    rhs.Add(kvp.Value);
                }

                tasks.Clear();

                var lhsDictionary = new Dictionary<Tuple<int, int>, int>();
                var tasksDictionary = new Dictionary<int, double>();
                double[,] lhs = new double[count, count];
                foreach (CoordFunction funcI in simpleFunctions)
                {
                    foreach (CoordFunction funcJ in functions)
                    {
                        Task task = null;
                        task = Task.Factory.StartNew((functionsArray) =>
                        {
                            var list = functionsArray as List<CoordFunction>;
                            tasksDictionary[task.Id] = Gauss.Integrate((double ro, double phi,
                                double a, double b, double m, double r, double uinf) =>
                            {
                                return Common.Instance.makeReplacement(ro, phi, list[0]) * Common.Instance.makeReplacement(ro, phi, list[1]) * Common.Instance.jac(ro, phi);
                            });
                            this.ReportProgress();
                        }, new List<CoordFunction>() { funcI, funcJ }, CancellationToken.None, TaskCreationOptions.PreferFairness, TaskScheduler.Default);

                        tasks.Add(task);
                        lhsDictionary.Add(Tuple.Create(simpleFunctions.IndexOf(funcI), functions.IndexOf(funcJ)), task.Id);
                    }
                }

                // spin wait
                Task.WaitAll(tasks.ToArray());

                foreach (var kvp in lhsDictionary)
                {
                    lhs[kvp.Key.Item1, kvp.Key.Item2] = tasksDictionary[kvp.Value];
                }
                               
                int info;
                double[] alphas;
                alglib.densesolverreport report;
                alglib.rmatrixsolve(lhs, count, rhs.ToArray(), out info, out report, out alphas);

                // Chop
                for (int i = 0; i < count; ++i)
                {
                    if (Math.Abs(alphas[i]) < this.eps)
                    {
                        alphas[i] = 0;
                    }
                }

                this.Result = alphas;
            });
        }

        public void DoLinear()
        {
            List<CoordFunction> simpleFunctions = Functions.GetCoordFunctions();
            List<CoordFunction> functions = Functions.GetOperatorCoordFunctions();
            int count = functions.Count;
            this.ActionsCount = count * count + count + 1; // nxn интегралов-левая часть, n интегралов-правая часть, 1-решение СЛАУ

            this.Worker.DoWork += new DoWorkEventHandler((object sender, DoWorkEventArgs e) =>
            {

                Task[] tasks = new Task[2];
                List<double> rhs = new List<double>(functions.Count);

                tasks[0] = Task.Factory.StartNew(() =>
                {
                    for (int i = 0; i < functions.Count; ++i)
                    {
                        function = simpleFunctions[i];
                        rhs.Add(-Gauss.Integrate(makeRhs)); //минус-потому что потом переносим в правую часть системы 
                        this.ReportProgress();
                    }
                });

                double[,] lhs = new double[count, count];

                tasks[1] = Task.Factory.StartNew(() =>
                {
                    for (int i = 0; i < count; ++i)
                    {
                        for (int j = 0; j < count; ++j)
                        {
                            currentI = simpleFunctions[i];
                            currentJ = functions[j];
                            lhs[i, j] = Gauss.Integrate(makeLhs);
                            this.ReportProgress();
                        }
                    }
                });

                Task.WaitAll(tasks);


                int info;
                double[] alphas;
                alglib.densesolverreport report;
                alglib.rmatrixsolve(lhs, count, rhs.ToArray(), out info, out report, out alphas);
                this.Progress++;
                this.Worker.ReportProgress(this.Progress);
                // Chop
                for (int i = 0; i < count; ++i)
                {
                    if (Math.Abs(alphas[i]) < this.eps)
                    {
                        alphas[i] = 0;
                    }
                }

                this.Result = alphas;
            });
        }

        #endregion

        private double makeRhs(double ro, double phi, double a, double b, double m, double r, double uinf)
        {
            return Common.Instance.makeReplacement(ro, phi, GeneratedFunctions.f0) * Common.Instance.makeReplacement(ro, phi, function) * Common.Instance.jac(ro, phi);
        }

        private double makeLhs(double ro, double phi, double a, double b, double m, double r, double uinf)
        {
            return Common.Instance.makeReplacement(ro, phi, currentI) * Common.Instance.makeReplacement(ro, phi, currentJ) * Common.Instance.jac(ro, phi);
        }

        private void ReportProgress()
        {
            this.Progress++;
            this.Worker.ReportProgress(this.Progress);
        }
    }
}
