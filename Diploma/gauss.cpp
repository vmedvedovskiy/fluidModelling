#include "gauss.h"
double eps = -pow(10.0, 6.0);

double Integrate(coordFunction func)
{
	int count = 0;
	double multiplier = ((sqrt(M + 1) - 1) / 2) * M_PI_2;
	double sum = 0.0;
	for(int i = 0; i < NNGauss; ++i)
	{
		for(int j = 0; j < NNGauss; ++j)
		{
			double value = func((sqrt(M + 1) + 1) / 2 + (sqrt(M + 1) - 1) / 2 * T[i], M_PI_2 + M_PI_2 * T[j]);
			sum += CG[i] * CG[j] * value;
		}
	}

	return multiplier * sum;
}
