namespace Livit.Data.Specifications
{
    using Livit.Data.Extensions;
    using System;
    using System.Linq.Expressions;

    public class NotSpecification<T> : CompositeSpecification<T>
    {
        private readonly ISpecification<T> _other;

        public NotSpecification(ISpecification<T> other)
        {
            this._other = other;
        }

        public override bool IsSatisfiedBy(T candidate)
        {
            return ToExpression().Compile().Invoke(candidate);
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            return this._other.ToExpression().Not();
        }
    }
}