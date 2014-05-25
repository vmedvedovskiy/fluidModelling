
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

        public IList<Function> Construct(Variable r, Variable th)
        {
            // IList<double> vals = new List<Double>();

            var all = new List<Function>();
            all = all
                .Concat(F1().Select(x => {
                    var omega2 = new Omega2();
                    // vals.Add(Integration.Integrate(Compiler.Compile(x.GetExpression(x.R, x.Th) * omega2.GetExpression(x.R, x.Th), x.R, x.Th)));
                    return x.GetExpression(r, th) * omega2.GetExpression(r, th);
                }))
                .Concat(F2().Select(x => {
                    var omega3 = new Omega3();
                    // vals.Add(Integration.Integrate(Compiler.Compile(x.GetExpression(x.R, x.Th) * omega3.GetExpression(x.R, x.Th), x.R, x.Th)));
                    return x.GetExpression(r, th) * omega3.GetExpression(r, th);
                }))
                .ToList();
            return all;
        }

        #endregion
    }
}
