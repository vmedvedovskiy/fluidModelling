
namespace Diploma.GUI
{
    using Diploma.Functions;
    using Diploma.Managed;
    using FuncLib.Functions;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string Result { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public Nonlinear Alghoritm { get; set; }

        public MainWindow()
        {
            this.Alghoritm = new Nonlinear();
            InitializeComponent();
        }

        private void Button_Click(object cat, RoutedEventArgs eve)
        {
            this.Reset();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            this.Alghoritm.Compute();
            this.ProgressBar.Maximum = this.Alghoritm.ActionsCount;
            this.Alghoritm.Worker.ProgressChanged += new ProgressChangedEventHandler((object sender, ProgressChangedEventArgs e) =>
            {
                this.ProgressBar.Value = e.ProgressPercentage;
                this.time.Content = stopwatch.Elapsed.ToString();
            });

            this.Alghoritm.Worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler((object sender, RunWorkerCompletedEventArgs e) =>
            {
                this.Result = string.Format("{{ {0} }}", string.Join(" ", this.Alghoritm.Result.Select(x => x.ToString("0.0000000"))).Replace(",", ".").Replace(" ", ",\n "));
                this.ResultLabel.AppendText(this.Result);
                stopwatch.Stop();
                this.EnableAll();
            });

            this.Alghoritm.Worker.RunWorkerAsync();
        }

        private void Reset()
        {
            this.ResultLabel.Clear();
            this.ProgressBar.Value = 0;
            this.M.IsEnabled = false;
            this.A.IsEnabled = false;
            this.B.IsEnabled = false;
            this.R.IsEnabled = false;
            this.Uinf.IsEnabled = false;
            this.NNGauss.IsEnabled = false;
        }

        private void EnableAll()
        {
            this.M.IsEnabled = true;
            this.A.IsEnabled = true;
            this.B.IsEnabled = true;
            this.R.IsEnabled = true;
            this.Uinf.IsEnabled = true;
            this.NNGauss.IsEnabled = true;
        }
    }
}
