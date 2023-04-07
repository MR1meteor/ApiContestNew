using System.Linq.Expressions;

namespace ApiContestNew.Core.Specifications
{
    public abstract class BaseSpecification<T> : ISpecifications<T>
    {
        public Expression<Func<T, bool>>? Criteria { get; private set; }
        public List<Expression<Func<T, object>>> IncludeExpressions { get; } = new();
        public Expression<Func<T, object>>? OrderByExpression { get; private set; }
        public Expression<Func<T, object>>? OrderByDescendingExpression { get; private set; }

        protected BaseSpecification(Expression<Func<T, bool>>? criteria)
        {
            Criteria = criteria;
        }

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            IncludeExpressions.Add(includeExpression);
        }

        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderByExpression = orderByExpression;
        }

        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
        {
            OrderByDescendingExpression = orderByDescendingExpression;
        }
    }
}
