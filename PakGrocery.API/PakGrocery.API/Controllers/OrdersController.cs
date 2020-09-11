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
        public class OrdersController : ControllerBase
        {
            private readonly IOrdersRepository _ordersRepository;
            private readonly IMapper _mapper;
            public OrdersController(IOrdersRepository ordersRepository, IMapper mapper)
            {
                this._ordersRepository = ordersRepository;
                this._mapper = mapper;

            }
            [HttpGet]
            public ActionResult<IEnumerable<Models.Order>> Get(string sort = "id", int pageNumber = 1, int pageSize = 10)
            {
                var list = this._ordersRepository.GetAll(sort);
                if (!list.Any())
                {
                    return NotFound();
                }

                var models = this._mapper.Map<IEnumerable<Models.Order>>(list);
                return Ok(models);
            }

            [HttpGet("{id}")]
            public ActionResult<IEnumerable<Models.Order>> Get(string id)
            {
                var order = this._ordersRepository.Find(id);
                if (order == null)
                {
                    return NotFound();
                }
                var model = this._mapper.Map<Models.Order>(order);
                return Ok(model);
            }
            [HttpPost]
            public IActionResult Post([FromBody] Models.Order order)
            {
                if (order == null)
                {
                    return BadRequest();
                }
                var ord = this._mapper.Map<Entities.Order>(order);

                ord.CreatedIP = "1.1.1.1";

                var result = this._ordersRepository.Insert(ord);

                if (result != null)
                {
                    return Created("api/orders", result.Id);
                }
                else
                {
                    return BadRequest("Failed to create order.");
                }

            }
            [HttpPut("{id}")]
            public IActionResult Put(string id, [FromBody] Models.Order order)
            {
                if (order == null)
                {
                    return BadRequest();
                }
                var ord = this._mapper.Map<Entities.Order>(order);

                ord.ModifiedIP = "2.2.2.2";

                var result = this._ordersRepository.Update(id, ord);

                if (result)
                {
                    var item = this._ordersRepository.Find(id);
                    return Ok(item);
                }
                else
                {
                    return BadRequest("Failed to Update order.");
                }
            }
            [HttpDelete("{id}")]
            public IActionResult Delete(string id, string sort = "id")
            {
                var order = this._ordersRepository.Delete(id);

                if (order)
                {
                    var list = this._ordersRepository.GetAll(sort);
                    if (!list.Any())
                    {
                        return NotFound();
                    }

                    var models = this._mapper.Map<IEnumerable<Models.Order>>(list);
                    return Ok(models);
                }
                else
                {
                    return BadRequest("Failed to Delete order.");
                }
            }
        }
}