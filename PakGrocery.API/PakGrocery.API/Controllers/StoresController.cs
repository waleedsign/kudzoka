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
    public class StoresController : ControllerBase
    {
        private readonly IStoresRepository _storesRepository;
        private readonly IMapper _mapper;
        public StoresController(IStoresRepository storesRepository, IMapper mapper)
        {
            this._storesRepository = storesRepository;
            this._mapper = mapper;

        }
        [HttpGet]
        public ActionResult<IEnumerable<Models.Store>> Get(string sort = "id", int pageNumber = 1, int pageSize = 10)
        {
            var list = this._storesRepository.GetAll(sort);
            if (!list.Any())
            {
                return NotFound();
            }

            var models = this._mapper.Map<IEnumerable<Models.Store>>(list);
            return Ok(models);
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Models.Store>> Get(string id)
        {
            var store = this._storesRepository.Find(id);
            if (store == null)
            {
                return NotFound();
            }
            var model = this._mapper.Map<Models.Store>(store);
            return Ok(model);
        }
        [HttpPost]
        public IActionResult Post([FromBody] Models.Store store)
        {
            if (store == null)
            {
                return BadRequest();
            }
            var s = this._mapper.Map<Entities.Store>(store);

            s.CreatedIP = "1.1.1.1";

            var result = this._storesRepository.Insert(s);

            if (result != null)
            {
                return Created("api/stores", result.Id);
            }
            else
            {
                return BadRequest("Failed to create store.");
            }
        }
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Models.Store store)
        {
            if (store == null)
            {
                return BadRequest();
            }
            var s = this._mapper.Map<Entities.Store>(store);

            s.ModifiedIP = "2.2.2.2";

            var result = this._storesRepository.Update(id, s);

            if (result)
            {
                var item = this._storesRepository.Find(id);
                return Ok(item);
            }
            else
            {
                return BadRequest("Failed to Update store.");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(string id, string sort = "id")
        {
            var store = this._storesRepository.Delete(id);

            if (store)
            {
                var list = this._storesRepository.GetAll(sort);
                if (!list.Any())
                {
                    return NotFound();
                }

                var models = this._mapper.Map<IEnumerable<Models.Store>>(list);
                return Ok(models);
            }
            else
            {
                return BadRequest("Failed to Delete store.");
            }
        }
    }
}