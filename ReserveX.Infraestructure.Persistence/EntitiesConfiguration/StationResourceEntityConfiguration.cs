using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReserveX.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserveX.Infraestructure.Persistence.EntitiesConfiguration
{
    internal class StationResourceEntityConfiguration : IEntityTypeConfiguration<StationResource>
    {
        public void Configure(EntityTypeBuilder<StationResource> builder)
        {
          builder.HasKey(x => x.Id);
        }
    }
}
