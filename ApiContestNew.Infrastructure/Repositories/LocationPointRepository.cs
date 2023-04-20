using ApiContestNew.Core.Interfaces.Repositories;
using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Core.Models.Filters;
using ApiContestNew.Core.Specifications.LocationPoint;
using ApiContestNew.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiContestNew.Infrastructure.Repositories
{
    public class LocationPointRepository : BaseRepository<LocationPoint>, ILocationPointRepository
    {
        public LocationPointRepository(DataContext dbContext)
            : base(dbContext)
        {
            
        }

        async public Task<LocationPoint?> GetPointByIdAsync(long id)
        {
            return await ApplySpecification(new PointByIdWithAll(id))
                .FirstOrDefaultAsync();
        }
        
        public async Task<LocationPoint?> GetPointByCoordsAsync(double latitude, double longitude)
        {
            return await ApplySpecification(new PointByCoordsWithAll(latitude, longitude))
                .FirstOrDefaultAsync();
        }

        public async Task<LocationPoint?> GetPointByFilterAsync(LocationPointFilter filter)
        {
            return await ApplySpecification(new PointByFilter(filter))
                .FirstOrDefaultAsync();
        }

        public async Task<LocationPoint?> AddPointAsync(LocationPoint point)
        {
            _dbContext.LocationPoints.Add(point);
            await _dbContext.SaveChangesAsync();
            return await GetPointByIdAsync(point.Id);
        }

        public async Task<LocationPoint?> UpdatePointAsync(LocationPoint point)
        {
            var editablePoint = await GetPointByIdAsync(point.Id);
            #pragma warning disable CS8602 // Dereference of a possibly null reference.
            editablePoint.Latitude = point.Latitude;
            editablePoint.Longitude = point.Longitude;
            #pragma warning restore CS8602 // Dereference of a possibly null reference.
            await _dbContext.SaveChangesAsync();
            return await GetPointByIdAsync(editablePoint.Id);
        }

        public async Task<LocationPoint?> DeletePointAsync(LocationPoint point)
        {
            _dbContext.Remove(point);
            await _dbContext.SaveChangesAsync();
            return null;
        }
    }
}
