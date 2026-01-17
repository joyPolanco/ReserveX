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
    public class RefreshTokenEntityConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(x=> x.Id);
            builder.Property(x=>x.UserId).IsRequired();
            builder.Property(x=>x.ExpiresAt).IsRequired();
            builder.Property(x => x.TokenHash).IsRequired();
            builder.Property(x => x.IsRevoked).IsRequired();
            builder.Property(x => x.RevokedAt).IsRequired(false);
            builder.Property(x => x.ReplacedByTokenHash).IsRequired(false);

        }

    }
}
