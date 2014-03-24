namespace Diploma.Functions
{
    using FuncLib.Functions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ProjectionFunctions : CoordinateFunctionsBase
    {
        #region Fields

        private static volatile ProjectionFunctions instance;
        private static readonly Object syncRoot = new Object();
        #endregion

        #region Constructors

        private ProjectionFunctions()
        {
        }

        static ProjectionFunctions()
        {

        }

        #endregion

        #region Properties

        public static ProjectionFunctions Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new ProjectionFunctions();
                        }
                    }
                }

                return instance;
            }
        }

        #endregion

        #region Methods

        protected override IList<Function> ConstructInternal(int m1, int m2, Variable r, Variable theta)
        {
            return CoordinateFunctions.Instance.GetRawTau(m1, m2, r, theta)
                .Select(x => Function.Pow(this.omega, 2) * x).ToList();
        }

        #endregion
    }
}
