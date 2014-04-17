

namespace Diploma.Functions
{
    using FuncLib.Functions;
    using System;

    public static class Operators
    {
        #region Properties

        public static Function E(Function psi, Variable r, Variable th)
        {
            return psi.Derivative(r, 2) + Function.Sin(th) / Function.Pow(r, 2) *
                (1 / Function.Sin(th) * psi.Derivative(th, 1)).Derivative(th, 1);
        }

        // Checked
        public static Function E2(Function psi, Variable r, Variable th)
        {
            return E(E(psi, r, th), r, th);
        }

        public static Function B(Function psi, Variable r, Variable th)
        {
            return ((1 / (Function.Pow(r, 2) * Function.Sin(th))) *
                (psi.Derivative(th, 1) * E(psi, r, th).Derivative(r, 1)
                    - psi.Derivative(r, 1) * E(psi, r, th).Derivative(th, 1))
                + (1 / (Function.Pow(r, 2) * Function.Sin(th))) * (2 * Common.Cot(th) * psi.Derivative(r, 1)
                    - 2 / r * psi.Derivative(th))) * E(psi, r, th);

        }

        #endregion
    }
}
