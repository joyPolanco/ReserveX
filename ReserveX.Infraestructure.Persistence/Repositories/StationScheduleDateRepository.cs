using ReserveX.Core.Domain.Entities;
using ReserveX.Core.Domain.Interfaces;

namespace ReserveX.Infraestructure.Persistence.Repositories
{
    public class StationScheduleDateRepository : GenericRepository<StationScheduleDate>, IStationScheduleDateRepository
    {
        public StationScheduleDateRepository(Contexts.AppDbContext context) : base(context)
        {
        }
    }
}
