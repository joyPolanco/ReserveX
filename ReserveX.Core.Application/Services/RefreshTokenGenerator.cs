using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ReserveX.Core.Application.Dtos.RefreshToken;
using ReserveX.Core.Application.Interfaces;
using ReserveX.Core.Domain.Entities;
using ReserveX.Core.Domain.Interfaces;
using ReserveX.Core.Domain.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ReserveX.Core.Application.Services
{
    public class RefreshTokenGenerator: IRefreshTokenGenerator
    {
        IRefreshTokenRepository _refreshTokenRepository;
        private RefreshTokensSettings _refreshTokenSettings;
        private readonly IMapper _mapper;

        public RefreshTokenGenerator(IRefreshTokenRepository refreshTokenRepository, IOptions<RefreshTokensSettings> options , IMapper mapper)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _refreshTokenSettings = options.Value;
            _mapper=mapper;
        }

        private string GenerateRefreshToken()
        {
            var randomBytes = new Byte[64];
            using var randomGenerator = RandomNumberGenerator.Create();
            randomGenerator.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }
        public  async Task<RefreshTokenDto> CreateRefreshToken(Guid UserId)
        {
          var refreshToken= GenerateRefreshToken();
            var expiresAt = DateTime.UtcNow.AddDays(_refreshTokenSettings.ExpirationTimeInDays);
           await _refreshTokenRepository.AddAsync(new Domain.Entities.RefreshToken
            {
                UserId = UserId,
                ExpiresAt = expiresAt,
                TokenHash = HashToken(refreshToken),
                Id=0,
                IsRevoked=false
            });
            return new RefreshTokenDto { Token= refreshToken, ExpiresAt=expiresAt };
        }
        private string HashToken (string token)
        {
            var bytes= Encoding.UTF8.GetBytes(token);

            using var encoder = SHA3_256.Create();
            var hash=encoder.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
        public async Task<RefreshToken?> FindToken (string token)
        {
            var hashed= HashToken(token);
           var row= await _refreshTokenRepository.GetAllQuery().Where(x => x.TokenHash == hashed).FirstOrDefaultAsync();
            if (row == null) return null;
            return row;
        }

        public async Task ReplaceTokenAsync(string newToken, RefreshToken oldTokenEntity)
        {
            oldTokenEntity.IsRevoked = true;
            oldTokenEntity.RevokedAt = DateTime.UtcNow;

            oldTokenEntity.ReplacedByTokenHash=HashToken(newToken);

            await _refreshTokenRepository.UpdateAsync(oldTokenEntity.Id,oldTokenEntity);
        }
       
    }
}
