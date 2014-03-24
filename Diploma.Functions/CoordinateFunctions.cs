
namespace Diploma.Functions
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using FuncLib.Functions;

    public class CoordinateFunctions: CoordinateFunctionsBase
    {
        #region Fields

        private static volatile CoordinateFunctions instance;
        private static readonly Object syncRoot = new Object();
        #endregion

        #region Constructors

        private CoordinateFunctions()
        {
        }

        static CoordinateFunctions()
        {

        }

        #endregion

        #region Properties

        public static CoordinateFunctions Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new CoordinateFunctions();
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
            var omega2 = Function.Pow(this.omega, 2);
            var omega3 = Function.Pow(this.omega, 2) * (1 - this.omega);
            IList<Function> all = new List<Function>();
            all = all
                .Concat(this.F1(r, theta, m1).Select(x => omega2 * x))
                .Concat(this.F2(r, theta, m2).Select(x => omega3 * x))
                .ToList();
            return all;
        }

        internal IList<Function> GetRawTau(int m1, int m2, Variable r, Variable theta)
        {
            this.omega = this.CreateOmega(r, theta);
            this.psi0 = this.CreatePsi0(r, theta);
            IList<Function> all = new List<Function>();
            all = all
                .Concat(this.F1(r, theta, m1))
                .Concat(this.F2(r, theta, m2))
                .ToList();
            return all;
        }

        private IList<Function> F1(Variable r, Variable theta, int m1)
        {
            IList<Function> result = new List<Function>();
            if (m1 % 2 != 0)
            {
                throw new ArgumentException("M1 must be even.");
            }

            int len = m1 / 2;
            for (int i = 1; i <= len; ++i)
            {
                result.Add(Function.Pow(r, -i) * this.J(i + 1, Function.Cos(theta), theta));
                result.Add(Function.Pow(r, -i) * this.J(i + 3, Function.Cos(theta), theta));
            }

            return result;
        }

        private IList<Function> F2(Variable r, Variable theta, int m2)
        {
            IList<Function> result = new List<Function>();
            result.Add(this.J(3, Function.Cos(theta), theta));
            result.Add(r * this.J(2, Function.Cos(theta), theta));

            if (m2 % 2 != 0)
            {
                throw new ArgumentException("M1 must be even.");
            }

            int len = (m2 - 2) /2 ;
            for (int i = 1; i <= len; ++i)
            {
                result.Add(Function.Pow(r, i) * this.J(i, Function.Cos(theta), theta));
                result.Add(Function.Pow(r, i + 2) * this.J(i, Function.Cos(theta), theta));
            }

            return result;
        }

        #endregion
    }
}
