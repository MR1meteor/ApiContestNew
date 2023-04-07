using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Core.Specifications;
using ApiContestNew.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiContestNew.Infrastructure.Repositories
{
    public abstract class BaseRepository<T>
        where T: BaseEntity
    {
        protected readonly DataContext _dbContext;

        public BaseRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected IQueryable<T> ApplySpecification(ISpecifications<T> specification)
        {
            return SpecificationEvaluator.GetQuery(
                _dbContext.Set<T>(),
                specification);
        }
    }
}
