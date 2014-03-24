

namespace Diploma.Functions
{
    using FuncLib.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    public class Operators
    {
         #region Fields

        private static volatile Operators instance;
        private static readonly Object syncRoot = new Object();
        #endregion

        #region Constructors

        private Operators()
        {
        }

        static Operators()
        {

        }

        #endregion

        #region Properties

        public static Operators Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new Operators();
                        }
                    }
                }

                return instance;
            }
        }

        public Function E(Function psi, Variable r, Variable th)
        {
            return psi.Derivative(r, 2) + Function.Sin(th) / Function.Pow(r, 2) *
                (1 / Function.Sin(th) * psi.Derivative(th, 1)).Derivative(th, 1);
        }

        public Function E2(Function psi, Variable r, Variable th)
        {
            return E(E(psi, r, th), r, th);
        }

        public Function B(Function psi, Variable r, Variable th)
        {
            return ((1 / (Function.Pow(r, 2) * Function.Sin(th))) *
                (psi.Derivative(th, 1) * this.E(psi, r, th).Derivative(r, 1)
                    - psi.Derivative(r, 1) * this.E(psi, r, th).Derivative(th, 1))
                + (1 / (Function.Pow(r, 2) * Function.Sin(th))) * (2 * Common.Cot(th) * psi.Derivative(r, 1)
                    - 2 / r * psi.Derivative(th))) * this.E(psi, r, th);

        }

        #endregion
    }
}
