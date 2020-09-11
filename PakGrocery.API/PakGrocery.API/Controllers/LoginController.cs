using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PakGrocery.API.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PakGrocery.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LoginController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;
        private IConfiguration _config;

        public LoginController(IUsersRepository usersRepository, IMapper mapper, IConfiguration config)
        {
            this._usersRepository = usersRepository;
            this._mapper = mapper;
            this._config = config;
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]Models.UserLogin user)
        {
            IActionResult response = Unauthorized();
            var auser = AuthenticateUser(user);

            if (auser != null)
            {
                var tokenString = GenerateJSONWebToken(auser);
                response = Ok(new { token = tokenString });
            }

            return response;
        }
        private string GenerateJSONWebToken(Models.User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private Models.User AuthenticateUser(Models.UserLogin user)
        {
            var auser = this._usersRepository.Find (u => (u.Name == user.UserNameOrEmail && u.Password == user.Password)||(u.Email == user.UserNameOrEmail && u.Password == user.Password)).FirstOrDefault();
            if (auser == null)
            {
                return null;
            }
            var model = this._mapper.Map<Models.User>(auser);
            return model;
        }
    }
}

