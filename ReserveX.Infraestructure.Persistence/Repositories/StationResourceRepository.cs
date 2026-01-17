using ReserveX.Core.Domain.Entities;
using ReserveX.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserveX.Infraestructure.Persistence.Repositories
{
    public class StationResourceRepository : GenericRepository<StationResource>, IStationResourceRepository
    {
        public StationResourceRepository(Contexts.AppDbContext context) : base(context)
        {
        }
    }
}
