using ApiContestNew.Core.Interfaces.Repositories;
using ApiContestNew.Core.Interfaces.Services;
using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Core.Models.Responses;

namespace ApiContestNew.Application.Services
{
    public class AreaService : IAreaService
    {
        private readonly IAreaRepository _areaRepository;

        public AreaService(IAreaRepository areaRepository)
        {
            _areaRepository = areaRepository;
        }

        async public Task<ServiceResponse<Area>> GetAreaAsync(long id)
        {
            if (id <= 0)
            {
                return new ServiceResponse400<Area>();
            }

            var area = await _areaRepository.GetAreaByIdAsync(id);

            if (area == null)
            {
                return new ServiceResponse404<Area>();
            }

            return new ServiceResponse200<Area>(data: area);
        }
    }
}
