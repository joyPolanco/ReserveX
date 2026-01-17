using ReserveX.Core.Application.Dtos.RefreshToken;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserveX.Core.Application.Dtos.Login
{
    public class TokenResponseDto
    {
        public required string AccessToken { get; set; }
        public required RefreshTokenDto RefreshToken { get; set; }
    }
}
