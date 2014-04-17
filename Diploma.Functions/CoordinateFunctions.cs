
namespace Diploma.Functions
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using FuncLib.Functions;

    public class CoordinateFunctions: CoordinateFunctionsBase
    {

        #region Methods

        protected override IList<Function> ConstructInternal(int m1, int m2, Variable r, Variable theta)
        {
            var omega2 = Function.Pow(omega, 2);
            var omega3 = Function.Pow(omega, 2) * (1 - omega);
            IList<Function> all = new List<Function>();
            all = all
                .Concat(F1(r, theta, m1).Select(x => omega2 * x))
                .Concat(F2(r, theta, m2).Select(x => omega3 * x))
                .ToList();
            return all;
        }

        internal static IList<Function> GetRawTau(int m1, int m2, Variable r, Variable theta)
        {
            omega = CreateOmega(r, theta);
            psi0 = CreatePsi0(r, theta);
            IList<Function> all = new List<Function>();
            all = all
                .Concat(F1(r, theta, m1))
                .Concat(F2(r, theta, m2))
                .ToList();
            return all;
        }

        private static IList<Function> F1(Variable r, Variable theta, int m1)
        {
            IList<Function> result = new List<Function>();
            if (m1 % 2 != 0)
            {
                throw new ArgumentException("M1 must be even.");
            }

            int len = m1 / 2;
            for (int i = 1; i <= len; ++i)
            {
                result.Add(Function.Pow(r, -i) * J(i + 1, Function.Cos(theta), theta));
                result.Add(Function.Pow(r, -i) * J(i + 3, Function.Cos(theta), theta));
            }

            return result;
        }

        private static IList<Function> F2(Variable r, Variable theta, int m2)
        {
            IList<Function> result = new List<Function>();
            result.Add(J(3, Function.Cos(theta), theta));
            result.Add(r * J(2, Function.Cos(theta), theta));

            if (m2 % 2 != 0)
            {
                throw new ArgumentException("M2 must be even.");
            }

            int len = (m2 - 2) /2 ;
            for (int i = 1; i <= len; ++i)
            {
                result.Add(Function.Pow(r, i) * J(i, Function.Cos(theta), theta));
                result.Add(Function.Pow(r, i + 2) * J(i, Function.Cos(theta), theta));
            }

            return result;
        }

        #endregion
    }
}
