using ReserveX.Core.Domain.Entities;
using ReserveX.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserveX.Infraestructure.Persistence.Repositories
{
    public class StationScheduleRepository : GenericRepository<StationSchedule>, IStationScheduleRepository
    {
        public StationScheduleRepository(Contexts.AppDbContext context) : base(context)
        {
        }
    }
}
