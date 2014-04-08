
namespace Diploma.Managed
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;


    internal class Integration
    {
        private static double[] T = { -0.998866, -0.994032, -0.985354, -0.972864, -0.956611, -0.936657, -0.913079, -0.885968, -0.85543, -0.821582, -0.784556, -0.744494, -0.701552, -0.655896, -0.607703, -0.557158, -0.504458, -0.449806, -0.393414, -0.3355, -0.276288, -0.216007, -0.154891, -0.0931747, -0.0310983, 0.0310983, 0.0931747, 0.154891, 0.216007, 0.276288, 0.3355, 0.393414, 0.449806, 0.504458, 0.557158, 0.607703, 0.655896, 0.701552, 0.744494, 0.784556, 0.821582, 0.85543, 0.885968, 0.913079, 0.936657, 0.956611, 0.972864, 0.985354, 0.994032, 0.998866 };
        private static double[] CG = { 0.00290862, 0.0067598, 0.0105905, 0.0143808, 0.0181156, 0.0217802, 0.0253607, 0.028843, 0.0322137, 0.0354598, 0.0385688, 0.0415285, 0.0443275, 0.0469551, 0.0494009, 0.0516557, 0.0537106, 0.0555577, 0.0571899, 0.0586008, 0.0597851, 0.060738, 0.0614559, 0.0619361, 0.0621766, 0.0621766, 0.0619361, 0.0614559, 0.060738, 0.0597851, 0.0586008, 0.0571899, 0.0555577, 0.0537106, 0.0516557, 0.0494009, 0.0469551, 0.0443275, 0.0415285, 0.0385688, 0.0354598, 0.0322137, 0.028843, 0.0253607, 0.0217802, 0.0181156, 0.0143808, 0.0105905, 0.0067598, 0.00290862 };

        internal static double Integrate(CoordFunction func)
        {
            #region caching variables

            double halfPi = Math.PI * 0.5;
            double m = Common.Instance.M;
            double a = Common.Instance.A;
            double b = Common.Instance.B;
            double r = Common.Instance.R;
            double uinf = Common.Instance.Uinf;
            double NNGauss = Common.Instance.NNGauss;
            double c1 = Math.Sqrt(m + 1) + 1;
            double c2 = Math.Sqrt(m + 1) - 1;
            double multiplier = ((Math.Sqrt(m + 1) - 1) / 2) * halfPi;

            #endregion

            double sum = 0.0;
            for (int i = 0; i < NNGauss - 1; ++i)
            {
                for (int j = 0; j < NNGauss - 1; ++j)
                {
                    double value = func(c1 / 2 + c2 / 2 * T[i], halfPi + halfPi * T[j], a, b, m, r, uinf);
                    sum += CG[i] * CG[j] * value;
                }
            }

            return multiplier * sum;
        }

        internal static void RefreshCoefficients(int n)
        {
            int info;
            double[] x;
            double[] w;
            alglib.gqgenerategausslegendre(n, out info, out x, out w);
            T = x;
            CG = w;
        }
    }
}
