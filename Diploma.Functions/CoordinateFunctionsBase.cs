

namespace Diploma.Functions
{
    using System.Collections.Generic;
    using FuncLib.Functions;
    using System;

    public abstract class CoordinateFunctionsBase
    {
        #region Fields

        private IList<Function> coordFunctions;
        protected Function psi0;
        protected Function omega;

        private static volatile CoordinateFunctionsBase instance;
        private static readonly Object syncRoot = new Object();
        private static readonly Object collectionRoot = new Object(); 

        #endregion

        #region Constructors

        protected CoordinateFunctionsBase()
        {
        }

        static CoordinateFunctionsBase()
        {

        }

        #endregion

        #region Properties

        public IList<Function> Functions
        {
            get
            {
                if (this.coordFunctions == null)
                {
                    lock (collectionRoot)
                    {
                        if (this.coordFunctions == null)
                        {
                            this.coordFunctions = new List<Function>();
                        }
                    }
                }

                return this.coordFunctions;
            }
        }

        #endregion

        #region Methods

        public void InitPsi0(double UNum, double RNum)
        {
            Variable U = new Variable();
            Variable R = new Variable();
            this.psi0 = this.psi0.PartialValue(U | UNum, R | RNum);
        }

        public void InitOmega(double A, double B)
        {
            Variable a = new Variable();
            Variable b = new Variable();
            this.omega = this.omega.PartialValue(a | A, b | B);
        }

        protected Function CreatePsi0(Variable r, Variable th)
        {
            Variable U = new Variable();
            Variable R = new Variable();
            return 0.25 * U * Function.Pow(r - R, 2) * (2 + (R / r)) *
                Function.Pow(Function.Sin(th), 2);
        }

        protected Function CreateOmega(Variable r, Variable th)
        {
            Variable a = new Variable();
            Variable b = new Variable();
            return ((Function.Pow(r, 2)) * Function.Pow(Function.Cos(th), 2)) / Function.Pow(a, 2) +
                ((Function.Pow(r, 2)) * Function.Pow(Function.Sin(th), 2)) / Function.Pow(b, 2) - 1;
        }

        protected Function J(int n, Function r, Variable v)
        {
            Variable x = new Variable();
            Function result = null;
            if (n == 0)
            {
                result = 1;
            }
            else if (n == 1)
            {
                result = -x;
            }
            else
            {
                result = -1 / fact(n - 1)
                    * (Function.Pow((Function.Pow(r, 2) - 1) / 2, n - 1).Derivative(v, n - 2));
            }

            return result;
        }

        protected abstract IList<Function> ConstructInternal(int m1, int m2, Variable r, Variable theta);

        public IList<Function> Construct(int m1, int m2)
        {
            Variable r = new Variable();
            Variable theta = new Variable();

            this.omega = this.CreateOmega(r, theta);
            this.psi0 = this.CreatePsi0(r, theta);

            return this.ConstructInternal(m1, m2, r, theta);
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

        #endregion
    }
}
