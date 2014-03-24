namespace Diploma.Functions
{
    using FuncLib.Functions;

    public class Common
    {
        public static Function Cot(Variable a)
        {
            return Function.Cos(a) / Function.Sin(a);
        }
    }
}
