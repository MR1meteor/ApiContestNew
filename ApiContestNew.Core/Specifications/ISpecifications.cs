using System.Linq.Expressions;

namespace ApiContestNew.Core.Specifications
{
    public interface ISpecifications<T>
    {
        public Expression<Func<T, bool>>? Criteria { get; }
        public List<Expression<Func<T, object>>> IncludeExpressions { get; }
        public Expression<Func<T, object>>? OrderByExpression { get; }
        public Expression<Func<T, object>>? OrderByDescendingExpression { get; }
    }
}
