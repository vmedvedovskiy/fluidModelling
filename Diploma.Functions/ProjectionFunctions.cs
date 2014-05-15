﻿namespace Diploma.Functions
{
    using FuncLib.Functions;
    using System.Collections.Generic;
    using System.Linq;

    public class ProjectionFunction : CoordinateFunctionsBase
    {

        public IList<IVariabledFunction> Construct(int m1, int m2)
        {
            // IList<double> vals = new List<Double>();

            var all = new List<IVariabledFunction>();
            all = all
                .Concat(F1(m1).Select(x =>
                {
                    var omega2 = new Omega2();
                    // vals.Add(Integration.Integrate(Compiler.Compile(x.GetExpression(x.R, x.Th) * omega2.GetExpression(x.R, x.Th), x.R, x.Th)));
                    return x.Compose(omega2);
                }))
                .Concat(F2(m2).Select(x =>
                {
                    var omega2 = new Omega3();
                    // vals.Add(Integration.Integrate(Compiler.Compile(x.GetExpression(x.R, x.Th) * omega3.GetExpression(x.R, x.Th), x.R, x.Th)));
                    return x.Compose(omega2);
                }))
                .ToList();
            return all;
        }
    }
}
