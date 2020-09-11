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
    public class PromotionsController : ControllerBase
    {
        private readonly IPromotionsRepository _promotionsRepository;
        private readonly IMapper _mapper;
        public PromotionsController(IPromotionsRepository promotionsRepository, IMapper mapper)
        {
            this._promotionsRepository = promotionsRepository;
            this._mapper = mapper;

        }
        [HttpGet]
        public ActionResult<IEnumerable<Models.Promotion>> Get(string sort = "id", int pageNumber = 1, int pageSize = 10)
        {
            var list = this._promotionsRepository.GetAll(sort);
            if (!list.Any())
            {
                return NotFound();
            }

            var models = this._mapper.Map<IEnumerable<Models.Promotion>>(list);
            return Ok(models);
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Models.Promotion>> Get(string id)
        {
            var promotion = this._promotionsRepository.Find(id);
            if (promotion == null)
            {
                return NotFound();
            }
            var model = this._mapper.Map<Models.Promotion>(promotion);
            return Ok(model);
        }
        [HttpPost]
        public IActionResult Post([FromBody] Models.Promotion promotion)
        {
            if (promotion == null)
            {
                return BadRequest();
            }
            var prom = this._mapper.Map<Entities.Promotion>(promotion);

            prom.CreatedIP = "1.1.1.1";

            var result = this._promotionsRepository.Insert(prom);

            if (result != null)
            {
                return Created("api/promotions", result.Id);
            }
            else
            {
                return BadRequest("Failed to create promotion.");
            }
        }
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Models.Promotion promotion)
        {
            if (promotion == null)
            {
                return BadRequest();
            }
            var prom = this._mapper.Map<Entities.Promotion>(promotion);

            prom.ModifiedIP = "2.2.2.2";

            var result = this._promotionsRepository.Update(id, prom);

            if (result)
            {
                var item = this._promotionsRepository.Find(id);
                return Ok(item);
            }
            else
            {
                return BadRequest("Failed to Update promotion.");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(string id, string sort = "id")
        {
            var promotion = this._promotionsRepository.Delete(id);

            if (promotion)
            {
                var list = this._promotionsRepository.GetAll(sort);
                if (!list.Any())
                {
                    return NotFound();
                }

                var models = this._mapper.Map<IEnumerable<Models.Promotion>>(list);
                return Ok(models);
            }
            else
            {
                return BadRequest("Failed to Delete promotion.");
            }
        }
    }
}