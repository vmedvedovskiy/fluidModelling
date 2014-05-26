namespace Diploma.Functions
{
    using FuncLib.Functions;
    using System.Collections.Generic;
    using System.Linq;

    public class ProjectionFunction : CoordinateFunctionsBase
    {

        public IList<Function> Construct(Variable r, Variable th)
        {
            var all = new List<Function>();
            all = all
                .Concat(F1().Select(x =>
                {
                    var omega2 = new Omega2();
                    return x.GetExpression(r, th) * omega2.GetExpression(r, th);
                }))
                .Concat(F2().Select(x =>
                {
                    var omega3 = new Omega3();
                    return x.GetExpression(r, th) * omega3.GetExpression(r, th);
                }))
                .ToList();
            return all;
        }
    }
}
