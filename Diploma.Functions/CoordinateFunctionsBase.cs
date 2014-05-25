

namespace Diploma.Functions
{
    using System.Collections.Generic;
    using FuncLib.Functions;
    using System;

    public abstract class CoordinateFunctionsBase
    {
        private static IList<VariabledFunction> f1;
        private static IList<VariabledFunction> f2;

        static CoordinateFunctionsBase()
        {
            f1 = new List<VariabledFunction>();
            f2 = new List<VariabledFunction>();
        }

        public static IList<VariabledFunction> F1()
        {
            if (f1.Count == 0 || f2.Count == 0)
            {
                Construct();
            }

            return f1;
        }

        public static IList<VariabledFunction> F2()
        {
            if (f1.Count == 0 || f2.Count == 0)
            {
                Construct();
            }

            return f2;
        }

        private static void Construct()
        {
            for (int i = 2; i <= Common.Instance.N; ++i)
            {
                var idx = GetIndexes(i);
                for (int j = 0; j < idx.Count; ++j)
                {
                    if (idx[j] >= 0)
                    {
                        f2.Add(new F(idx[j], i));
                    }
                    else
                    {
                        f1.Add(new F(idx[j], i));
                    }
                }
            }
        }

        private static IList<int> GetIndexes(int n)
        {
            return new List<int>
            {
                n, 1 - n, n + 2, 3 - n
            };
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

    internal class F : VariabledFunction
    {
        private int pow;
        private int j;

        public F(int pow, int j)
            : base()
        {
            this.pow = pow;
            this.j = j;
        }

        public override Function GetExpression(Variable r, Variable th)
        {
            return Function.Pow(r, this.pow) * Common.Instance.J(this.j, Function.Cos(th), th);
        }
    }

    internal class Psi0 : VariabledFunction
    {
        public override Function GetExpression(Variable r, Variable th)
        {
            var o2 = new Omega2();
            return o2.GetExpression(r, th) * 0.5 * Common.Instance.Uinf *
                Function.Pow(r, 2) * Function.Pow(Function.Sin(th), 2);
        }
    }

}
