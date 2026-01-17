using ReserveX.Core.Application.Dtos.RefreshToken;
using ReserveX.Core.Domain.Entities;
using ReserveX.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ReserveX.Core.Application.Interfaces
{
    public interface IRefreshTokenGenerator
    {


        public  Task<RefreshTokenDto> CreateRefreshToken(Guid UserId);
        Task<RefreshToken> FindToken(string token);
        Task ReplaceTokenAsync(string newToken, RefreshToken oldTokenEntity);
    }
}
