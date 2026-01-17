using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserveX.Core.Application.Interfaces
{
    public interface IJwtTokenGenerator 
    {
        public string GenerateAccessToken(Guid userId, string email, string role);
       
    }

}
