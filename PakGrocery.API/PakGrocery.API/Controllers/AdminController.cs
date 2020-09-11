using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PakGrocery.API.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PakGrocery.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdminController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;
        public AdminController(IUsersRepository usersRepository, IMapper mapper)
        {
            this._usersRepository = usersRepository;
            this._mapper = mapper;

        }

        public IActionResult GetInactiveRiders()
        {
            var riders = this._usersRepository.Find(u => u.UserTypeId == 4 && u.IsActive == false);

            if (!riders.Any())
            {
                return NotFound("No inactive Riders found.");
            }

            var ridersModel = this._mapper.Map<IEnumerable<Models.User>>(riders);

            return Ok(ridersModel);
        }

        [HttpPut]
        public IActionResult ActivateRider(string id)
        {
            var riders = this._usersRepository.Find(u => u.Id == id);

            if (!riders.Any())
            {
                return NotFound("Rider not found.");
            }

            //var ridersModel = this._mapper.Map<IEnumerable<Models.User>>(riders);
            var bResult = this._usersRepository.ActivateRider(id);

            if (bResult)
                return Ok("Rider activated successfully.");

            return StatusCode(500, "INternal server error while activating rider.");
        }
    }
}
