using ReserveX.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserveX.Core.Application.Dtos.RefreshToken
{
    public class RefreshTokenDto
    {
        public string? TokenHash { get; set; }

        public required string Token { get; set; }
        public DateTime ExpiresAt { get; set; }

        public Guid UserId { get; set; }
       
    }
}
