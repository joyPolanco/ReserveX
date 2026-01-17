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
    public class StationEntityCofiguration : IEntityTypeConfiguration<Station>
    {
        public void Configure(EntityTypeBuilder<Station> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Description).IsRequired(false);
            builder.Property(x => x.SlotDurationMinutes).IsRequired();
            builder.Property(x=>x.SlotsCapacity).IsRequired();
            builder.Property(x => x.Status);

            builder.HasMany(x => x.Slots)
                .WithOne(x => x.Station)
                .HasForeignKey(x => x.StationId);

            builder.HasMany(x => x.StationResources)
              .WithOne(x => x.Station)
              .HasForeignKey(x => x.StationId);

            builder.HasMany(x => x.Schedules)
              .WithOne(x => x.Station)
              .HasForeignKey(x => x.StationId);



        }
    }
}
