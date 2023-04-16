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

        public async Task<Area?> GetAreaByNameAsync(string name)
        {
            return await ApplySpecification(new AreaByNameWithPoints(name))
                .FirstOrDefaultAsync();
        }

        public async Task<Area?> AddAreaAsync(Area area)
        {
            _dbContext.Areas.Add(area);
            await _dbContext.SaveChangesAsync();

            return await GetAreaByIdAsync(area.Id);
        }

        public async Task<Area?> DeleteAreaAsync(Area area)
        {
            _dbContext.Areas.Remove(area);
            await _dbContext.SaveChangesAsync();

            return null;
        }
    }
}
