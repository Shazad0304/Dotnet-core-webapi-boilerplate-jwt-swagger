using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Cronicle.DBContexts;
using Cronicle.Models;
using Cronicle.Services;
using Cronicle.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Cronicle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private ProjectDbContext DBContext;

        public LoginController(IConfiguration config, ProjectDbContext context)
        {
            _config = config;
            DBContext = context;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] Login login)
        {
            IActionResult response = null;
            var user = AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }
            else {
                response = CustomResponse.NotFound();
            }

            return response;
        }

        private string GenerateJSONWebToken(Users userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim("UserId", userInfo.UserId.ToString())
                };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(87600),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private Users AuthenticateUser(Login login)
        {
            // you auttenctication login

            return null;
        }
    }
}