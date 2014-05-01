

namespace Diploma.Functions
{
    using FuncLib.Functions;
    using System;

    public abstract class BaseOperator
    {
        public abstract Function Apply(Function f, Variable r, Variable th);
    }

    public class E : BaseOperator
    {
        public override Function Apply(Function f, Variable r, Variable th)
        {
 	        return f.Derivative(r, 2) + Function.Sin(th) / Function.Pow(r, 2) *
                (1 / Function.Sin(th) * f.Derivative(th, 1)).Derivative(th, 1);
        }
    }

    public class E2 : BaseOperator
    {
        public override Function Apply(Function f, Variable r, Variable th)
        {
            var e = new E();
 	        return e.Apply(e.Apply(f, r, th), r, th);
        }
    }

    public class B : BaseOperator
    {

        private static Function Cot(Variable a)
        {
            return Function.Cos(a) / Function.Sin(a);
        }

        public override Function Apply(Function f, Variable r, Variable th)
        {
            var e = new E();
 	        return ((1 / (Function.Pow(r, 2) * Function.Sin(th))) *
                (f.Derivative(th, 1) * e.Apply(f, r, th).Derivative(r, 1)
                    - f.Derivative(r, 1) * e.Apply(f, r, th).Derivative(th, 1))
                + (1 / (Function.Pow(r, 2) * Function.Sin(th))) * (2 * Cot(th) * f.Derivative(r, 1)
                    - 2 / r * f.Derivative(th))) * e.Apply(f, r, th);
        }
    }
}
