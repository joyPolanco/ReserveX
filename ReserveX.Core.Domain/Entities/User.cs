using ReserveX.Core.Domain.Common.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserveX.Core.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }= Guid.NewGuid();
        public required string Name { get; set; }

        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public UserRole Role { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    }
}
