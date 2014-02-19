//(*1. �������� �� ����� �-���*)
//(*2. ��������� "��������"*)
//(*3. �������� ���������� ��������� �� ������ ������� �� \
//������������������ s*)
//(*4. ������ ����������, ��������� �� �������*)
//(*5. ���������� ������*)
//(*6. ������ ���������� ����. ���������-����� ������������� �����*)

#include "alghoritm.h"

coordFunction function = NULL;
coordFunction currentI = NULL;
coordFunction currentJ = NULL;

double makeRhs(double ro, double phi)
{
	return makeReplacement(ro, phi, f0) * makeReplacement(ro, phi, function) * jac(ro, phi);
}

double makeLhs(double ro, double phi)
{
    return makeReplacement(ro, phi, currentI) * makeReplacement(ro, phi, currentJ) * jac(ro, phi);
}

void DoWork(double* result, int nSize)
{
	vector<coordFunction> functions;
	CreateVectorOfFunctions(functions);
	vector<coordFunction> simpleFunctions;
	CreateVectorOfSimpleFunctions(simpleFunctions);

	int count =  functions.size();
	int* rhs = new int[count];

	for(int i = 0; i < count - 1; ++i)
	{
		function = functions[i];
		rhs[i] = -Integrate(makeRhs);
	}

	double** lhs = new double*[count];
	for(int k = 0; k < count; ++k)
	{
		lhs[k] = new double[count];
	}

    for (int i = 0; i < count; ++i)
    {
        for (int j = 0; j < count; ++j)
        {
            currentI = simpleFunctions[i];
            currentJ = functions[j];
            lhs[i][j] = Integrate(makeLhs);
        }
    }
}

