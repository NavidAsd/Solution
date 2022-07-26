using Application.Interface.FacadPattern;
using Common;
using EndPoint.Models.Validation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUsersFacad _userFacad;
        private readonly IConfiguration _Config;
        public UserController(IUsersFacad userFacad, IConfiguration config)
        {
            _userFacad = userFacad;
            _Config = config;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] Models.UserLoginViewModel user)
        {
            #region Validation
            try
            {
                UserValidation validation = new UserValidation();
                ValidationResult Result = validation.Validate(user);
                if (!Result.IsValid)
                {
                    foreach (FluentValidation.Results.ValidationFailure failure in Result.Errors)
                    {
                        return StatusCode(400, new ResultMessage
                        {
                            Success = false,
                            Message = failure.ErrorMessage,
                        });
                    }
                }
            }
            catch
            {
                return StatusCode(500, new ResultMessage
                {
                    Success = false,
                    Message = "Something Wrong Please Try Again",
                });
            }
            #endregion
            var result = await _userFacad.UsersLoginService.ExecuteAsynce(new Application.Services.Query.UserLogin.RequestUsersDto
            {
                UserName = user.UserName,
                Password = user.Password
            });
            if (result.Success)
                return Ok(GenerateToken(user.UserName));
            return Unauthorized();
        }
        [HttpGet("UsersInfo")]
        //[Authorize]
        public async Task<ActionResult> UsersInfo()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return Ok(await _userFacad.ReturnUsersDataService.GetUsersAsynce());
            return Unauthorized();
        }

        private string GenerateToken(string UserName)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,UserName)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_Config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_Config["Jwt:Issuer"],
                _Config["Jwt:Audience"],
                claims: claims,
                expires: System.DateTime.Now.AddDays(7), // a week
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
