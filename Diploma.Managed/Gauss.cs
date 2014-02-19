
namespace Diploma.Managed
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;


    public class Gauss
    {
        public static double Integrate(CoordFunction func)
        {
            double multiplier = ((Math.Sqrt(Common.Instance.M + 1) - 1) / 2) * Math.PI * 0.5;
            double sum = 0.0;
            for (int i = 0; i < Common.NNGauss - 1; ++i)
            {
                for (int j = 0; j < Common.NNGauss - 1; ++j)
                {
                    double value = func((Math.Sqrt(Common.Instance.M + 1) + 1) / 2 + (Math.Sqrt(Common.Instance.M + 1) - 1) / 2 * Common.T[i], Math.PI * 0.5 + Math.PI * 0.5 * Common.T[j], 
                        Common.Instance.A, Common.Instance.B, Common.Instance.M, Common.Instance.R, Common.Instance.Uinf);
                    sum += Common.CG[i] * Common.CG[j] * value;
                }
            }

            return multiplier * sum;
        }
    }
}
