namespace Diploma.Functions
{
    using FuncLib.Functions;
    using System.Collections.Generic;
    using System.Linq;

    public class ProjectionFunctions : CoordinateFunctionsBase
    {
        #region Methods

        protected override IList<Function> ConstructInternal(int m1, int m2, Variable r, Variable theta)
        {
            return CoordinateFunctions.GetRawTau(m1, m2, r, theta)
                .Select(x => Function.Pow(omega, 2) * x).ToList();
        }

        #endregion
    }
}
