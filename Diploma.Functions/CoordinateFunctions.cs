
namespace Diploma.Functions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using FuncLib.Functions;

    public class CoordinateFunctions
    {
        private Function psi0;
        private Function omega;

        public CoordinateFunctions()
        {
            this.CreatePsi0();
        }

        public void InitPsi0(double U, double R)
        {
            Variable u = new Variable();
            Variable r = new Variable();
            this.psi0 = this.psi0.PartialValue(u | U, r | R);
        }

        public void InitOmega(double a, double b)
        {
            Variable A = new Variable();
            Variable B = new Variable();
            this.omega = this.omega.PartialValue(A | a, B | b);
        }

        public static IList<Function> Construct(int m1, int m2)
        {
        }

        private void CreatePsi0()
        {
            Variable r = new Variable();
            Variable th = new Variable();
            Variable U = new Variable();
            Variable R = new Variable();
            this.psi0 = 0.25 * U * Function.Pow(r - R, 2) * (2 + (R / r)) *
                Function.Pow(Function.Sin(th), 2);
        }

        private void CreateOmega()
        {
            Variable r = new Variable();
            Variable th = new Variable();
            Variable a = new Variable();
            Variable b = new Variable();
            this.omega = ((Function.Pow(r, 2)) * Function.Pow(Function.Cos(th), 2)) / Function.Pow(a, 2) +
                ((Function.Pow(r, 2)) * Function.Pow(Function.Sin(th), 2)) / Function.Pow(b, 2) - 1;
        }

        private Function J(int n, Function x)
        {
            if (n == 0)
                return 1;
            if (n == 1)
                return -x;
            return -1 / fact(n - 1) * (Function.Pow((Function.Pow(x, 2) - 1) / 2, n - 1).Derivative(x, n - 2));
        }

        private double fact(int n)
        {
            double result = 1;
            while (n > 1)
            {
                result = result * n;
                n--;
            }

            return result;
        }
    }
}
