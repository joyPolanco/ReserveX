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
    public class StationScheduleEntityConfiguration : IEntityTypeConfiguration<StationSchedule>
    {
        public void Configure(EntityTypeBuilder<StationSchedule> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.StartTime);
            builder.Property(x => x.EndTime);
            builder.Property(x => x.Status);

            builder.HasMany(x => x.Dates)
                .WithOne(x => x.StationSchedule)
                .HasForeignKey(x => x.StationScheduleId);
        }
    }
}
