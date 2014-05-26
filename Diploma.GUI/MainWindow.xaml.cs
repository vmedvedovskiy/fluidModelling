
namespace Diploma.GUI
{
    using Diploma.Managed;
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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

            this.Alghoritm.Worker.ProgressChanged += (sender, e) =>
            {
                this.ProgressBar.Value = e.ProgressPercentage;
                this.time.Content = stopwatch.Elapsed.ToString();
            };

            this.Alghoritm.IterationCompleted += (sender, e) =>
            {
                Dispatcher.BeginInvoke(new Action(() => {
                    this.AppendResultButton(e);
                }));
            };

            this.Alghoritm.ComputationCompleted += (sender, e) =>
            {
                this.CompletedText.Visibility = System.Windows.Visibility.Visible;
            };

            this.Alghoritm.Worker.RunWorkerAsync();
        }

        private void AppendResultButton(IterationCompletedArgs e)
        {
            var row = new RowDefinition();
            row.Height = new GridLength(24, GridUnitType.Auto);

            this.ProgressBar.Value = 0;
            var button = new Button();
            button.Content = e.Number;
            button.Click += (bs, be) =>
            {
                Clipboard.SetDataObject(FormatOutput(e.Alphas));
            };

            var label = new Label();
            label.Content = e.Norm.ToString("{0.###E+000}");
            this.ButtonsPanel.RowDefinitions.Add(row);

            Grid.SetColumn(button, 0);
            Grid.SetColumn(label, 1);
            var rowIdx = this.ButtonsPanel.RowDefinitions.IndexOf(row);
            Grid.SetRow(button, rowIdx);
            Grid.SetRow(label, rowIdx);
            this.ButtonsPanel.Children.Add(button);
            this.ButtonsPanel.Children.Add(label);

        }

        private void Reset()
        {
            this.ProgressBar.Value = 0;
            this.M.IsEnabled = false;
            this.A.IsEnabled = false;
            this.B.IsEnabled = false;
            this.R.IsEnabled = false;
            this.Uinf.IsEnabled = false;
            this.NNGauss.IsEnabled = false;
            this.N.IsEnabled = false;
            this.BeginIteration.IsEnabled = false;
        }

        private void EnableAll()
        {
            this.M.IsEnabled = true;
            this.A.IsEnabled = true;
            this.B.IsEnabled = true;
            this.R.IsEnabled = true;
            this.Uinf.IsEnabled = true;
            this.NNGauss.IsEnabled = true;
            this.N.IsEnabled = true;
            this.BeginIteration.IsEnabled = true;
        }

        private static string FormatOutput(double[] input)
        {
            return string.Format("{{ {0} }}", string.Join(" ", input.Select(x => x.ToString("0.000000000"))).Replace(",", ".").Replace(" ", ","));
        }
    }
}
