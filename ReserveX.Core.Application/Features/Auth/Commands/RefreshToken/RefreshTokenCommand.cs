using MediatR;
using ReserveX.Core.Application.Dtos.Login;
using ReserveX.Core.Application.Dtos.RefreshToken;
using ReserveX.Core.Application.Interfaces;
using ReserveX.Core.Application.Services;
using ReserveX.Core.Domain.Common.enums;
using ReserveX.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ReserveX.Core.Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommand :IRequest<Result<TokenResponseDto>>
    {
        public string? RefreshToken { get; set; }
    }

    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<TokenResponseDto>>
    {
        private readonly IRefreshTokenGenerator _refreshTokenGenerator;
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public RefreshTokenCommandHandler(IRefreshTokenGenerator refreshTokenGenerator, IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _refreshTokenGenerator= refreshTokenGenerator;
            _userRepository= userRepository;
            _jwtTokenGenerator= jwtTokenGenerator;
        }
        public async Task<Result<TokenResponseDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {

            var searchedToken =await _refreshTokenGenerator.FindToken(request.RefreshToken!);
            if (searchedToken == null) return Result<TokenResponseDto>.Failure("Refresh token not found", ErrorType.NotFound);

            var user = _userRepository.GetAllQuery().Where(r=>r.Id==searchedToken.UserId).FirstOrDefault();
            if (user == null) return Result<TokenResponseDto>.Failure("User associated to refresh token not found", ErrorType.NotFound);

            var jwtToken= _jwtTokenGenerator.GenerateAccessToken(user.Id,user.Email, user.Role.ToString());

            var newRefreshToken = await _refreshTokenGenerator.CreateRefreshToken(user.Id);
             await _refreshTokenGenerator.ReplaceTokenAsync(newRefreshToken.Token, searchedToken);

            return Result<TokenResponseDto>.Success(new TokenResponseDto
            {
                AccessToken = jwtToken,
                RefreshToken = new RefreshTokenDto
                {
                    Token = newRefreshToken.Token,
                    ExpiresAt = newRefreshToken.ExpiresAt,
                }
            }
            );
        }
    }
}
