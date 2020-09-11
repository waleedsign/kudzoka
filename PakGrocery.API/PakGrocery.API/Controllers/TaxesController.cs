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
    public class TaxesController : ControllerBase
    {
        private readonly ITaxesRepository _taxesRepository;
        private readonly IMapper _mapper;
        public TaxesController(ITaxesRepository taxesRepository, IMapper mapper)
        {
            this._taxesRepository = taxesRepository;
            this._mapper = mapper;

        }
        [HttpGet]
        public ActionResult<IEnumerable<Models.Tax>> Get(string sort = "id", int pageNumber = 1, int pageSize = 10)
        {
            var list = this._taxesRepository.GetAll(sort);
            if (!list.Any())
            {
                return NotFound();
            }

            var models = this._mapper.Map<IEnumerable<Models.Tax>>(list);
            return Ok(models);
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Models.Tax>> Get(string id)
        {
            var tax = this._taxesRepository.Find(id);
            if (tax == null)
            {
                return NotFound();
            }
            var model = this._mapper.Map<Models.Tax>(tax);
            return Ok(model);
        }
        [HttpPost]
        public IActionResult Post([FromBody] Models.Tax tax)
        {
            if (tax == null)
            {
                return BadRequest();
            }
            var t = this._mapper.Map<Entities.Tax>(tax);

            t.CreatedIP = "1.1.1.1";

            var result = this._taxesRepository.Insert(t);

            if (result != null)
            {
                return Created("api/taxes", result.Id);
            }
            else
            {
                return BadRequest("Failed to create tax.");
            }
        }
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Models.Tax tax)
        {
            if (tax == null)
            {
                return BadRequest();
            }
            var t = this._mapper.Map<Entities.Tax>(tax);

            t.ModifiedIP = "2.2.2.2";

            var result = this._taxesRepository.Update(id, t);

            if (result)
            {
                var item = this._taxesRepository.Find(id);
                return Ok(item);
            }
            else
            {
                return BadRequest("Failed to Update tax.");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(string id, string sort = "id")
        {
            var tax = this._taxesRepository.Delete(id);

            if (tax)
            {
                var list = this._taxesRepository.GetAll(sort);
                if (!list.Any())
                {
                    return NotFound();
                }

                var models = this._mapper.Map<IEnumerable<Models.Tax>>(list);
                return Ok(models);
            }
            else
            {
                return BadRequest("Failed to Delete tax.");
            }
        }
    }
}