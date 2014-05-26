
namespace Diploma.Functions
{
    using FuncLib.Functions;
    using FuncLib.Functions.Compilation;
    using System;
    using System.ComponentModel;

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
        private double r = 1;
        private int nNGauss = 50;
        private int n = 5;
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

        public double Re
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
                    this.OnPropertyChanged("Re");
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

        public int N
        {
            get
            {
                return this.n;
            }
            set
            {
                int result;
                if (int.TryParse(value.ToString(), out result))
                {
                    n = result;
                    this.OnPropertyChanged("N");
                }
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
                result = -r;
            }
            else
            {
                result = (-1 / fact(n - 1))
                    * (Function.Pow((Function.Pow(v, 2) - 1) / 2, n - 1).Derivative(v, n - 2));

                var pv = new PartialValueEvaluation(Point.Empty);
                pv.AddPartialValue(v, r);
                result = result.PartialValue(pv);
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
            var cfs = new CoordinateFunctions().Construct(r, th);
            var result = new ConstantFunction(0) as Function;

            int i = 0;
            for (; i < cfs.Count; i++)
            {
                result = result + this.alpha[i] * cfs[i];
            }

            return result;
        }

        public void SetAlphas(double[] newAlphas)
        {
            if (newAlphas.Length != this.alpha.Length)
            {
                throw new ArgumentException("Length of alphas array must match");
            }

            this.alpha = newAlphas;
        }

        public double GetNorm()
        {
            return Integration.Integrate(Compiler.Compile(this.GetExpression(this.R, this.Th), this.R, this.Th));
        }
    }
}
