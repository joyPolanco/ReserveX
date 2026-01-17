using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserveX.Core.Domain.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public required string TokenHash { get; set; }
        public Guid UserId { get; set; }
        public bool IsRevoked { get; set; } = false;
        public DateTime ExpiresAt { get; set; }
        public DateTime ?RevokedAt { get; set; }
        public  string ?ReplacedByTokenHash { get; set; }


    }

}
