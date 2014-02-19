#pragma once

#include <cmath>
#include <vector>
#define _USE_MATH_DEFINES 1
#include "math.h"
#include "input.h"

const double PI = 3.1415926;
#define Sin sin
#define Cos cos
const double E = 2.71282;

typedef double (*coordFunction)(double t, double th);

inline double omega(double r, double th)
{
	return (pow(r,2) * pow(cos(th), 2)) / pow(a,2) + (pow(r,2) * pow(sin(th), 2)) / pow(b,2) - 1.0; 
}

inline double w(double r, double th)
{
	return 1.0 - exp((M * omega(r, th)) / (omega(r, th) - M));
}

inline double psi0(double r, double th)
{
	return 0.25 * (Uinf * pow(r - R, 2) * (2 + R / r) * pow(sin(th), 2));
}

inline double makeReplacement(double ro, double phi, coordFunction target)
{
	return target(ro * sqrt(pow(a, 2) * pow(cos(phi), 2) + pow(b, 2) * pow(sin(phi), 2)), atan2(b * sin(phi), a * cos(phi)));
}

inline double jac(double ro, double phi)
{
	return a * b * ro * pow(cos(phi), 2) + a * b * ro * pow(sin(phi), 2);
}

inline double Power(double arg1, int arg2)
{
	return pow(arg1, arg2);
}
