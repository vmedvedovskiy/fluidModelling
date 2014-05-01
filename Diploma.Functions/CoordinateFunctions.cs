
namespace Diploma.Functions
{
    using FuncLib.Functions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CoordinateFunctions
    {

        #region Methods

        public IList<IVariabledFunction> Construct(int m1, int m2)
        {
            var all = new List<IVariabledFunction>();
            all = all
                .Concat(F1(m1).Select(x => {
                    var omega2 = new Omega2();
                    return x.Compose(omega2);
                }))
                .Concat(F2(m2).Select(x => {
                    var omega3 = new Omega3();
                    return x.Compose(omega3);
                }))
                .ToList();
            return all;
        }

        internal static IList<Function> GetRawTau(int m1, int m2, Variable r, Variable theta)
        {
            IList<Function> all = new List<Function>();
            //all = all
            //    .Concat(F1(r, theta, m1))
            //    .Concat(F2(r, theta, m2))
            //    .ToList();
            return all;
        }

        private static IList<VariabledFunction> F1(int m1)
        {
            IList<VariabledFunction> result = new List<VariabledFunction>();
            if (m1 % 2 != 0)
            {
                throw new ArgumentException("M1 must be even.");
            }

            int len = m1 / 2;
            for (int i = 1; i <= len; ++i)
            {
                result.Add(new F(-i, i + 1));
                result.Add(new F(-i, i + 3));
            }

            return result;
        }

        private static IList<VariabledFunction> F2(int m2)
        {
            IList<VariabledFunction> result = new List<VariabledFunction>();
            result.Add(new F2_1());
            result.Add(new F2_2());

            if (m2 % 2 != 0)
            {
                throw new ArgumentException("M2 must be even.");
            }

            int len = (m2 - 2) /2 ;
            for (int i = 1; i <= len; ++i)
            {
                result.Add(new F(i, i));
                result.Add(new F(i + 2, i));
            }

            return result;
        }

        #endregion
    }

    internal class F : VariabledFunction
    {
        private int pow;
        private int j;

        public F(int pow, int j)
            :base()
        {
            this.pow = pow;
            this.j = j;
        }

        public override Function GetExpression(Variable r, Variable th)
        {
            return Function.Pow(r, this.pow) * Common.Instance.J(this.j, Function.Cos(th), th);
        }
    }

    internal class Omega2 : VariabledFunction
    {
        public override Function GetExpression(Variable r, Variable th)
        {
            var omega = new Omega();
            return Function.Pow(omega.GetExpression(r, th), 2);
        }
    }

    internal class Omega3 : VariabledFunction
    {
        public override Function GetExpression(Variable r, Variable th)
        {
            var omega = new Omega();
            return Function.Pow(omega.GetExpression(r, th), 2) * (1 - omega.GetExpression(r, th));
        }
    }

    internal class F2_1 : VariabledFunction
    {
        public override Function GetExpression(Variable r, Variable th)
        {
            return Common.Instance.J(3, Function.Cos(th), th);
        }
    }

    internal class F2_2 : VariabledFunction
    {
        public override Function GetExpression(Variable r, Variable th)
        {
            return r * Common.Instance.J(2, Function.Cos(th), th);
        }
    }

}
