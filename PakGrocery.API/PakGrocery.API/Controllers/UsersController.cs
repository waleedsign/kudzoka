using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PakGrocery.API.Repositories.Contracts;

namespace PakGrocery.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;
        public UsersController(IUsersRepository usersRepository, IMapper mapper)
        {
            this._usersRepository = usersRepository;
            this._mapper = mapper;

        }
        [HttpGet]
        public ActionResult<IEnumerable<Models.User>> Get(string sort = "id", int pageNumber = 1, int pageSize = 10)
        {
            var list = this._usersRepository.GetAll(sort);
            if (!list.Any())
            {
                return NotFound();
            }

            var models = this._mapper.Map<IEnumerable<Models.User>>(list);
            return Ok(models);
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Models.User>> Get(string id)
        {
            var user = this._usersRepository.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            var model = this._mapper.Map<Models.User>(user);
            return Ok(model);
        }
        [HttpPost]
        public IActionResult Post([FromBody] Models.User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            var u = this._mapper.Map<Entities.User>(user);

            u.CreatedIP = "1.1.1.1";

            var result = this._usersRepository.Insert(u);

            if (result != null)
            {
                return Created("api/users", result.Id);
            }
            else
            {
                return BadRequest("Failed to create user.");
            }
        }
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Models.User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            var u = this._mapper.Map<Entities.User>(user);

            u.ModifiedIP = "2.2.2.2";

            var result = this._usersRepository.Update(id, u);

            if (result)
            {
                var item = this._usersRepository.Find(id);
                return Ok(item);
            }
            else
            {
                return BadRequest("Failed to Update user.");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(string id, string sort = "id")
        {
            var user = this._usersRepository.Delete(id);

            if (user)
            {
                var list = this._usersRepository.GetAll(sort);
                if (!list.Any())
                {
                    return NotFound();
                }

                var models = this._mapper.Map<IEnumerable<Models.User>>(list);
                return Ok(models);
            }
            else
            {
                return BadRequest("Failed to Delete user.");
            }
        }

        [HttpPost]
        public IActionResult RegisterRider([FromBody]Models.User user)
        {
            if (string.IsNullOrEmpty(user.Name))
            {
                return BadRequest("Name is required.");
            }
            if (string.IsNullOrEmpty(user.Address))
            {
                return BadRequest("Address is required.");
            }
            if (string.IsNullOrEmpty(user.City))
            {
                return BadRequest("City is required.");
            }
            if (string.IsNullOrEmpty(user.Email))
            {
                return BadRequest("Email is required.");
            }
            if (string.IsNullOrEmpty(user.MobileNo))
            {
                return BadRequest("Mobile Number is required.");
            }
            if (string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("Password is required.");
            }
            if (string.IsNullOrEmpty(user.ConfirmPassword))
            {
                BadRequest("Confirm Password is required.");
            }
            if (user.Password != user.ConfirmPassword)
            {
                return BadRequest("Password does not matches.");
            }

            var auser = this._usersRepository.Find(u => u.MobileNo == user.MobileNo || u.Email == user.Email);
            if (auser != null)
            {
                return BadRequest("Rider already exists.");
            }

            var newUser = this._mapper.Map<Entities.User>(user);
            newUser.CreatedBy = "admin";
            newUser.CreatedIP = "1.1.1.1";
            newUser.IsActive = false;
            newUser.UserTypeId = 4;
            //4 - Rider
            //3 - User
            //2 - Vendor
            //1 - Admin

            var ent = this._usersRepository.Insert(newUser);

            if (ent != null)
            {
                var newModel = this._mapper.Map<Models.User>(ent);

                return Created("api/users/" + ent.Id, newModel);
            }

            return StatusCode(500, "Unable to save Rider. Please contact application administrator.");
        }
        [HttpPost]
        public IActionResult RegisterVendor([FromBody]Models.User user)
        {
            if (string.IsNullOrEmpty(user.Name))
            {
                return BadRequest("Name is required.");
            }
            if (string.IsNullOrEmpty(user.StoreName))
            {
                return BadRequest("Store Name is required.");
            }
            if (string.IsNullOrEmpty(user.StoreAddress))
            {
                return BadRequest("Store Address is required.");
            }
            if (string.IsNullOrEmpty(user.City))
            {
                return BadRequest("City is required.");
            }
            if (string.IsNullOrEmpty(user.Email))
            {
                return BadRequest("Email is required.");
            }
            if (string.IsNullOrEmpty(user.MobileNo))
            {
                return BadRequest("Mobile Number is required.");
            }
            if (string.IsNullOrEmpty(user.BusinessRegNo))
            {
                return BadRequest("Business registraion number is required.");
            }
            if (string.IsNullOrEmpty(user.StoreLogo))
            {
                return BadRequest("Store Logo is required.");
            }
            if (string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("Password is required.");
            }
            if (string.IsNullOrEmpty(user.ConfirmPassword))
            {
                BadRequest("Confirm Password is required.");
            }
            if (user.Password != user.ConfirmPassword)
            {
                return BadRequest("Password does not matches.");
            }

            var auser = this._usersRepository.Find(u => u.MobileNo == user.MobileNo || u.Email == user.Email);
            if (auser != null)
            {
                return BadRequest("Vendor already exists.");
            }

            var newUser = this._mapper.Map<Entities.User>(user);
            newUser.CreatedBy = "admin";
            newUser.CreatedIP = "1.1.1.1";
            newUser.IsActive = false;
            newUser.UserTypeId = 2;
            //4 - Rider
            //3 - User
            //2 - Vendor
            //1 - Admin

            var ent = this._usersRepository.Insert(newUser);

            if (ent != null)
            {
                var newModel = this._mapper.Map<Models.User>(ent);

                return Created("api/users/" + ent.Id, newModel);
            }

            return StatusCode(500, "Unable to save Vendor. Please contact application administrator.");
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody]Models.User user)
        {
            IActionResult response = Unauthorized();
            var auser = AuthenticateUser(user);

            if (auser != null)
            {
                var tokenString = GenerateJSONWebToken();
                response = Ok(new { token = tokenString });
            }

            return response;
        }
        //private string GenerateJSONWebToken(Models.User userInfo)
        //{
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(_config["Jwt:Issuer"],
        //      _config["Jwt:Issuer"],
        //      null,
        //      expires: DateTime.Now.AddMinutes(120),
        //      signingCredentials: credentials);

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}
        private IActionResult AuthenticateUser(Models.User user)
        {

            var auser = this._usersRepository.Find(u => (u.Name == user.Name && u.Password == user.Password) || (u.Email == user.Email && u.Password == user.Password));
            if (auser == null)
            {
                return NotFound("User does not exists.");
            }
            var model = this._mapper.Map<Models.User>(user);
            return Ok(model);
        }

        private string GenerateJSONWebToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("P@kGrocery@uthenticat!onT0k3n"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "https://www.test.com",
                audience: "https://www.test.com",
                expires: DateTime.Now.AddHours(3),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
