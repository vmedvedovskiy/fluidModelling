
namespace Diploma.Managed
{
    using System;
    using System.Diagnostics;
    using System.ComponentModel;


    public delegate double CoordFunction(double x, double y, double a, double b, double m, double R, double uinf);

    public class Common: INotifyPropertyChanged
    {

        #region Constructors

        private Common(){ }

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Fields

        private double a = 2;
        private double b = 1;
        private double m = 2;
        private double uinf = 1;
        private double r = 0;
        private int nNGauss = 50;

        private static volatile Common instance;
        private static object syncRoot = new Object();

        #endregion

        #region Properties

        public static Common Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new Common();
                    }
                }

                return instance;
            }
        }

        public double A
        {
            get
            {
                return this.a;
            }
            set
            {
                double result;
                if (double.TryParse(value.ToString(), out result))
                {
                    a = result;
                    this.OnPropertyChanged("A");
                }
            }
        }

        public double B 
        {
            get
            {
                return this.b;
            }
            set
            {
                double result;
                if (double.TryParse(value.ToString(), out result))
                {
                    b = result;
                    this.OnPropertyChanged("B");
                }
            }
        }

        public double M
        {
            get
            {
                return this.m;
            }
            set
            {
                double result;
                if (double.TryParse(value.ToString(), out result))
                {
                    m = result;
                    this.OnPropertyChanged("M");
                }
            }
        }

        public double Uinf
        {
            get
            {
                return this.uinf;
            }
            set
            {
                double result;
                if (double.TryParse(value.ToString(), out result))
                {
                    uinf = result;
                    this.OnPropertyChanged("Uinf");
                }
            }
        }

        public double R
        {
            get
            {
                return this.r;
            }
            set
            {
                double result;
                if (double.TryParse(value.ToString(), out result))
                {
                    r = result;
                    this.OnPropertyChanged("R");
                }
            }
        }

        public int NNGauss
        {
            get 
            {
                return nNGauss;
            }
            set 
            {
                int result;
                if (int.TryParse(value.ToString(), out result))
                {
                    nNGauss = result;
                    Integration.RefreshCoefficients(nNGauss);
                    this.OnPropertyChanged("NNGauss");
                }
            }
        }

        
        #endregion

        #region Methods

        public double omega(double r, double th)
        {
            return (Math.Pow(r, 2) * Math.Pow(Math.Cos(th), 2)) / Math.Pow(a, 2) + (Math.Pow(r, 2) * Math.Pow(Math.Sin(th), 2)) / Math.Pow(b, 2) - 1.0;
        }

        public double w(double r, double th)
        {
            var result = 1.0 - Math.Exp((M * omega(r, th)) / (omega(r, th) - M));
            if (result > 1)
            {
                Debugger.Break();
            }

            return result;
        }

        public double psi0(double r, double th)
        {
            return 0.25 * (Uinf * Math.Pow(r - R, 2) * (2 + R / r) * Math.Pow(Math.Sin(th), 2));
        }

        public double makeReplacement(double ro, double phi, CoordFunction target)
        {
            return target(ro * Math.Sqrt(Math.Pow(a, 2) * Math.Pow(Math.Cos(phi), 2) + Math.Pow(b, 2) * Math.Pow(Math.Sin(phi), 2)), Math.Atan2(b * Math.Sin(phi), a * Math.Cos(phi))
                ,Common.Instance.A, Common.Instance.B, Common.Instance.M, Common.Instance.R, Common.Instance.Uinf);
        }

        public double jac(double ro, double phi)
        {
            return a * b * ro * Math.Pow(Math.Cos(phi), 2) + a * b * ro * Math.Pow(Math.Sin(phi), 2);
        } 

        #endregion
    }
}
