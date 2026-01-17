using MediatR;
using ReserveX.Core.Application.Dtos.Login;
using ReserveX.Core.Application.Exceptions;
using ReserveX.Core.Application.Interfaces;
using ReserveX.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserveX.Core.Application.Features.Login.Commands.Login
{
    public class LoginCommand : IRequest<Result<TokenResponseDto>>
    {
        public string ?Email { get; set; }
        public string ?Password { get; set; }

    }


    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<TokenResponseDto>>
    
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IHasher _passwordHasher;
        private readonly IRefreshTokenGenerator _refreshTokenGenerator;

        public LoginCommandHandler(
            IUserRepository userRepository, 
            IJwtTokenGenerator jwtTokenGenerator,
            IRefreshTokenGenerator refreshTokenGenerator,
            IHasher passwordHasher
            )
        {
            _userRepository= userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _passwordHasher= passwordHasher;
            _refreshTokenGenerator= refreshTokenGenerator;

        }
        public async  Task<Result<TokenResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
           var user =  await _userRepository.GetByEmail(request.Email!);
            if (user == null) return Result<TokenResponseDto>.Failure("Incorrect email or password", Domain.Common.enums.ErrorType.Unauthorized);


            var passwordIsValid = _passwordHasher.CompareHash(request.Password!, user.PasswordHash);

            if(!passwordIsValid) return Result<TokenResponseDto>.Failure("Incorrect email or password", Domain.Common.enums.ErrorType.Unauthorized);

            var token = _jwtTokenGenerator.GenerateAccessToken(user.Id, user.Email, user.Role.ToString());
            var refreshToken = await _refreshTokenGenerator.CreateRefreshToken(user.Id);

            return Result<TokenResponseDto>.Success( new TokenResponseDto{ AccessToken = token, RefreshToken=refreshToken });


        }
    }
}
