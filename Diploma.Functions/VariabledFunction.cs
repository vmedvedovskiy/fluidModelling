namespace Diploma.Functions
{
    using FuncLib.Functions;
    using ImpromptuInterface;
    using ImpromptuInterface.Dynamic;

    public interface IVariabledFunction
    {
        Variable R { get; }

        Variable Th { get; }

        Function Expression { get; }

        Function GetExpression(Variable r, Variable th);

        IVariabledFunction Compose(IVariabledFunction another);

        double Value(double r, double th);
    }

    public abstract class VariabledFunction : IVariabledFunction
    {
        private Variable r;
        private Variable th;
        protected Function expression;

        public VariabledFunction()
        {
            this.r = new Variable();
            this.th = new Variable();
            this.expression = this.GetExpression(this.r, this.th);
        }

        public Variable R
        {
            get
            {
                return this.r;
            }
        }

        public Variable Th
        {
            get
            {
                return this.th;
            }
        }

        public Function Expression
        {
            get
            {
                return this.expression;
            }
        }

        public double Value(double r, double th)
        {
            return this.expression.Value(this.r | r, this.th | th);
        }

        public abstract Function GetExpression(Variable r, Variable th);

        public IVariabledFunction Compose(IVariabledFunction another)
        {
            return new
            {
                R = this.r,
                Th = this.th,
                Expression = this.expression * another.GetExpression(this.r, this.th),
                GetExpression = Return<Function>.Arguments<Variable, Variable>((r, th) =>
                {
                    return this.GetExpression(r, th);
                }),
                Compose = Return<IVariabledFunction>.Arguments<IVariabledFunction>((f) =>
                {
                    return this.Compose(f);
                }),
                Value = Return<double>.Arguments<double, double>((r, th) =>
                {
                    return this.Expression.Value(this.r | r, this.th | th);
                }),

            }.ActLike<IVariabledFunction>();
        }

    }
}
