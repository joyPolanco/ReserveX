using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using ReserveX.Core.Application;
using ReserveX.Core.Application.Dtos.Login;
using ReserveX.Core.Application.Features.Auth.Commands.RefreshToken;
using ReserveX.Core.Application.Features.Login.Commands.Login;

namespace WebApi.Controllers.v1
{
    [Route("api/v{version::apiVersion}/auth")]

    public class AuthController : BaseApiController
    {

        [HttpPost("/sign-in")]
        public async Task<IActionResult> SignIn(LoginCommand request)
        {
            var response = await _mediator.Send(request);
            if(!response.IsSuccess)
            {
                return HandleResult(response);
            }

            Response.Cookies.Append("refreshToken",
                response.Value!.RefreshToken.Token,
                new CookieOptions
                {
                    HttpOnly = true,
                    Expires = response.Value.RefreshToken.ExpiresAt,
                    Secure = true,
                    Path = "/auth",
                    SameSite = SameSiteMode.Strict
                });

            return Ok(new { accessToken = response.Value.AccessToken });
        }


        [HttpPost("/login")]
        public async Task <IActionResult> Login (LoginCommand request)
        {
            var response = await _mediator.Send(request);
            if (!response.IsSuccess)
            {
                return HandleResult(response);
            }

            Response.Cookies.Append("refreshToken",
                response.Value!.RefreshToken.Token,
                new CookieOptions
                {
                    HttpOnly = true,
                    Expires = response.Value.RefreshToken.ExpiresAt,
                    Secure = true,
                    Path = "/auth",
                    SameSite = SameSiteMode.Strict
                });
            return Ok(new {accessToken=response.Value.AccessToken});
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("/logout")]
        public async Task<IActionResult> Logout(LoginCommand request)
        {
            var response = await _mediator.Send(request);
            return Ok(new { accessToken = response });
        }


        [HttpPost("/refresh-token")]
        public async Task<IActionResult> RefreshToken(LoginCommand request)
        {
            Request.Cookies.TryGetValue("refreshToken", out var refreshToken);

            if (refreshToken == null) return BadRequest("RefreshToken missing, try log in");

            var command = new RefreshTokenCommand { RefreshToken= refreshToken };

            var response = await _mediator.Send(request);
            if (!response.IsSuccess)
            {
                return HandleResult(response);
            }

            Response.Cookies.Delete(refreshToken);
            Response.Cookies.Append("refreshToken",
               response.Value!.RefreshToken.Token,
               new CookieOptions
               {
                   HttpOnly = true,
                   Expires = response.Value.RefreshToken.ExpiresAt,
                   Secure = true,
                   Path = "/auth",
                   SameSite = SameSiteMode.Strict
               });
            return Ok(new { accessToken = response.Value.AccessToken});
        }

    }
}
