using ApiContestNew.Core.Interfaces.Repositories;
using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Core.Specifications.Area;
using ApiContestNew.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiContestNew.Infrastructure.Repositories
{
    public class AreaRepository : BaseRepository<Area>, IAreaRepository
    {
        public AreaRepository(DataContext dataContext)
            : base(dataContext)
        {

        }

        public async Task<Area?> GetAreaByIdAsync(long id)
        {
           return await ApplySpecification(new AreaByIdWithPoints(id))
                .FirstOrDefaultAsync();
        }
    }
}
