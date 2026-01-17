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
    public class SlotEntityConfiguration : IEntityTypeConfiguration<Slot>
    {
        public void Configure(EntityTypeBuilder<Slot> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Date).IsRequired();
            builder.Property(x=>x.StartTime).IsRequired();
            builder.Property(x => x.EndTime).IsRequired();
            builder.Property(x => x.TotalCapacity).IsRequired();
            builder.Property(x => x.AvailableCapacity).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.RowVersion).IsRowVersion().IsConcurrencyToken();

            builder.HasMany(x => x.Reservations)
                .WithOne(x => x.Slot)
                .HasForeignKey(x => x.SlotId);
    }
    }
}
