using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PakGrocery.API.Repositories.Contracts;

namespace PakGrocery.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserRolesController : ControllerBase
    {
        private readonly IUserRolesRepository _userRolesRepository;
        private readonly IMapper _mapper;
        public UserRolesController(IUserRolesRepository userRolesRepository, IMapper mapper)
        {
            this._userRolesRepository = userRolesRepository;
            this._mapper = mapper;

        }
        [HttpGet]
        public ActionResult<IEnumerable<Models.UserRole>> Get(string sort = "id", int pageNumber = 1, int pageSize = 10)
        {
            var list = this._userRolesRepository.GetAll(sort);
            if (!list.Any())
            {
                return NotFound();
            }

            var models = this._mapper.Map<IEnumerable<Models.UserRole>>(list);
            return Ok(models);
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Models.UserRole>> Get(string id)
        {
            var userRole = this._userRolesRepository.Find(id);
            if (userRole == null)
            {
                return NotFound();
            }
            var model = this._mapper.Map<Models.UserRole>(userRole);
            return Ok(model);
        }
        [HttpPost]
        public IActionResult Post([FromBody] Models.UserRole userRole)
        {
            if (userRole == null)
            {
                return BadRequest();
            }
            var ur = this._mapper.Map<Entities.UserRole>(userRole);

            ur.CreatedIP = "1.1.1.1";

            var result = this._userRolesRepository.Insert(ur);

            if (result != null)
            {
                return Created("api/userRoles", result.Id);
            }
            else
            {
                return BadRequest("Failed to create userRole.");
            }
        }
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Models.UserRole userRole)
        {
            if (userRole == null)
            {
                return BadRequest();
            }
            var ur = this._mapper.Map<Entities.UserRole>(userRole);

            ur.ModifiedIP = "2.2.2.2";

            var result = this._userRolesRepository.Update(id, ur);

            if (result)
            {
                var item = this._userRolesRepository.Find(id);
                return Ok(item);
            }
            else
            {
                return BadRequest("Failed to Update userRole.");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(string id, string sort = "id")
        {
            var userRole = this._userRolesRepository.Delete(id);

            if (userRole)
            {
                var list = this._userRolesRepository.GetAll(sort);
                if (!list.Any())
                {
                    return NotFound();
                }

                var models = this._mapper.Map<IEnumerable<Models.UserRole>>(list);
                return Ok(models);
            }
            else
            {
                return BadRequest("Failed to Delete userRole.");
            }
        }
    }
}