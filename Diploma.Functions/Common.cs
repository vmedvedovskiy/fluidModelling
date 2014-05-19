
namespace Diploma.Functions
{
    using System;
    using System.Diagnostics;
    using System.ComponentModel;
    using FuncLib.Functions;
    using System.Linq;
    using FuncLib.Functions.Compilation;

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
        private int m1 = 8;
        private int m2 = 8;
        private Function _jacobian;

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

        public int M1
        {
            get
            {
                return this.m1;
            }
            set
            {
                int result;
                if (int.TryParse(value.ToString(), out result))
                {
                    m1 = result;
                    this.OnPropertyChanged("M1");
                }
            }
        }

        public int M2
        {
            get
            {
                return this.m2;
            }
            set
            {
                int result;
                if (int.TryParse(value.ToString(), out result))
                {
                    m2 = result;
                    this.OnPropertyChanged("M2");
                }
            }
        }

        public Function Jacobian
        {
            get
            {
                if (this._jacobian == null)
                {
                    Variable r = new Variable();
                    Variable th = new Variable();

                    this._jacobian = a * b * r * Function.Pow(Function.Cos(th), 2)
                        + a * b * r * Function.Pow(Function.Sin(th), 2);
                }

                return this._jacobian;
            }
        }
        
        #endregion

        #region Methods

        public Function J(int n, Function r, Variable v)
        {
            Function result = null;
            if (n == 0)
            {
                result = 1;
            }
            else if (n == 1)
            {
                result = -v;
            }
            else
            {
                result = -1 / fact(n - 1)
                    * (Function.Pow((Function.Pow(r, 2) - 1) / 2, n - 1).Derivative(v, n - 2));
            }

            return result;
        }

        private double fact(int n)
        {
            double result = 1;
            while (n > 1)
            {
                result = result * n;
                n--;
            }

            return result;
        }

        public static Function Cot(Variable x)
        {
            return Function.Cos(x) / Function.Sin(x);
        }

        #endregion
    }

    public class Omega : VariabledFunction
    {
        public override Function GetExpression(Variable r, Variable th)
        {
            var omega = ((Function.Pow(r, 2)) * Function.Pow(Function.Cos(th), 2)) / Function.Pow(Common.Instance.A, 2) +
                    ((Function.Pow(r, 2)) * Function.Pow(Function.Sin(th), 2)) / Function.Pow(Common.Instance.B, 2) - 1;

            return 1 - Function.Exp(Common.Instance.M * omega / (omega - Common.Instance.M));
        }
    }

    public class U : VariabledFunction
    {
        private double[] alpha;

        public U(int count)
        {
            this.alpha = new double[count];
            for (int i = 0; i < count; ++i)
            {
                this.alpha[i] = 0;
            }
        }

        public override Function GetExpression(Variable r, Variable th)
        {
            var o2 = new Omega2();
            var o3 = new Omega3();
            var f1 = CoordinateFunctionsBase.F1(Common.Instance.M1);
            var f2 = CoordinateFunctionsBase.F2(Common.Instance.M2);
            var result = new ConstantFunction(0) as Function;

            int i = 0;
            for (; i < f1.Count; i++ )
            {
                result = result + this.alpha[i] * f1[i].GetExpression(r, th) * o2.GetExpression(r, th);
            }

            for (int j = 0; j < f2.Count; j++)
            {
                result = result + this.alpha[j + i] * f2[j].GetExpression(r, th) * o3.GetExpression(r, th);
            }

            return result;
        }
    }
}
