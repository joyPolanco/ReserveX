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
    public class StationScheduleDateEntityConfiguration : IEntityTypeConfiguration<StationScheduleDate>
    {
        public void Configure(EntityTypeBuilder<StationScheduleDate> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x=>x.Date).IsRequired();

        }
    }
}
