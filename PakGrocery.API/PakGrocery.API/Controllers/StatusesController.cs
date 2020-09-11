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
    public class StatusesController : ControllerBase
    {
        private readonly IStatusesRepository _statusesRepository;
        private readonly IMapper _mapper;
        public StatusesController(IStatusesRepository statusesRepository, IMapper mapper)
        {
            this._statusesRepository = statusesRepository;
            this._mapper = mapper;

        }
        [HttpGet]
        public ActionResult<IEnumerable<Models.Status>> Get(string sort = "id", int pageNumber = 1, int pageSize = 10)
        {
            var list = this._statusesRepository.GetAll(sort);
            if (!list.Any())
            {
                return NotFound();
            }

            var models = this._mapper.Map<IEnumerable<Models.Status>>(list);
            return Ok(models);
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Models.Status>> Get(string id)
        {
            var status = this._statusesRepository.Find(id);
            if (status == null)
            {
                return NotFound();
            }
            var model = this._mapper.Map<Models.Status>(status);
            return Ok(model);
        }
        [HttpPost]
        public IActionResult Post([FromBody] Models.Status status)
        {
            if (status == null)
            {
                return BadRequest();
            }
            var s = this._mapper.Map<Entities.Status>(status);

            s.CreatedIP = "1.1.1.1";

            var result = this._statusesRepository.Insert(s);

            if (result != null)
            {
                return Created("api/statuses", result.Id);
            }
            else
            {
                return BadRequest("Failed to create status.");
            }
        }
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Models.Status status)
        {
            if (status == null)
            {
                return BadRequest();
            }
            var s = this._mapper.Map<Entities.Status>(status);

            s.ModifiedIP = "2.2.2.2";

            var result = this._statusesRepository.Update(id, s);

            if (result)
            {
                var item = this._statusesRepository.Find(id);
                return Ok(item);
            }
            else
            {
                return BadRequest("Failed to Update status.");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(string id, string sort = "id")
        {
            var tax = this._statusesRepository.Delete(id);

            if (tax)
            {
                var list = this._statusesRepository.GetAll(sort);
                if (!list.Any())
                {
                    return NotFound();
                }

                var models = this._mapper.Map<IEnumerable<Models.Status>>(list);
                return Ok(models);
            }
            else
            {
                return BadRequest("Failed to Delete Status.");
            }
        }
    }
}
