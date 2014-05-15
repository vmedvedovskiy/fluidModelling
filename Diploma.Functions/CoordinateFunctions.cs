
namespace Diploma.Functions
{
    using FuncLib.Functions;
    using FuncLib.Functions.Compilation;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CoordinateFunctions : CoordinateFunctionsBase
    {

        #region Methods

        public IList<IVariabledFunction> Construct(int m1, int m2)
        {
            // IList<double> vals = new List<Double>();

            var all = new List<IVariabledFunction>();
            all = all
                .Concat(F1(m1).Select(x => {
                    var omega2 = new Omega2();
                    // vals.Add(Integration.Integrate(Compiler.Compile(x.GetExpression(x.R, x.Th) * omega2.GetExpression(x.R, x.Th), x.R, x.Th)));
                    return x.Compose(omega2);
                }))
                .Concat(F2(m2).Select(x => {
                    var omega3 = new Omega3();
                    // vals.Add(Integration.Integrate(Compiler.Compile(x.GetExpression(x.R, x.Th) * omega3.GetExpression(x.R, x.Th), x.R, x.Th)));
                    return x.Compose(omega3);
                }))
                .ToList();
            return all;
        }

        #endregion
    }
}
