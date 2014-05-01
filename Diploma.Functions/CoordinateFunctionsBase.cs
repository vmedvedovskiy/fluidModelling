

namespace Diploma.Functions
{
    using System.Collections.Generic;
    using FuncLib.Functions;
    using System;

    public class Omega : VariabledFunction
    {
        public override Function GetExpression(Variable r, Variable th)
        {
            var omega = ((Function.Pow(r, 2)) * Function.Pow(Function.Cos(th), 2)) / Function.Pow(Common.Instance.A, 2) +
                    ((Function.Pow(r, 2)) * Function.Pow(Function.Sin(th), 2)) / Function.Pow(Common.Instance.B, 2) - 1;

            return 1 - Function.Exp(Common.Instance.M * omega / (omega - Common.Instance.M));
        }
    }

}
