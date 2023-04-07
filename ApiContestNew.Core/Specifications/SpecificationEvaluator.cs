using ApiContestNew.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiContestNew.Core.Specifications
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<T> GetQuery<T>(
            IQueryable<T> inputQueryable,
            ISpecifications<T> specification)
            where T : BaseEntity
        {
            IQueryable<T> queryable = inputQueryable;

            if (specification.Criteria != null)
            {
                queryable = queryable.Where(specification.Criteria);
            }

            specification.IncludeExpressions.Aggregate(
                queryable,
                (current, includeExpression) => current.Include(includeExpression));

            foreach (var includeExpression in specification.IncludeExpressions) 
            {
                queryable = queryable.Include(includeExpression);
            }

            if (specification.OrderByExpression != null)
            {
                queryable = queryable.OrderBy(specification.OrderByExpression);
            }
            else if (specification.OrderByDescendingExpression != null)
            {
                queryable = queryable.OrderByDescending(specification.OrderByDescendingExpression);
            }

            return queryable;
        }
    }
}
