using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PakGrocery.API.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using PakGrocery.API.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace PakGrocery.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductsController: ControllerBase
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IMapper _mapper;
        public ProductsController(IProductsRepository productsRepository, IMapper mapper)
        {
            this._productsRepository = productsRepository;
            this._mapper = mapper;

        }
        [HttpGet]
        public ActionResult<IEnumerable<Models.Product>> Get(string sort = "id", int pageNumber = 1, int pageSize = 10 )
        {
            var list = this._productsRepository.GetAll(sort);
            if (!list.Any())
            {
                return NotFound();
            }
            
            var models = this._mapper.Map<IEnumerable<Models.Product>>(list);
            return Ok(models);
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Models.Product>> Get(string id)
        {
            var product = this._productsRepository.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            var model = this._mapper.Map<Models.Product>(product);
            return Ok(model);
        }
        [HttpPost]
        public IActionResult Post([FromBody] Models.Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            var prod = this._mapper.Map<Entities.Product>(product);

            prod.CreatedIP = "1.1.1.1";
            
            var result = this._productsRepository.Insert(prod);

            if (result != null)
            {
                return Created("api/products", result.Id);
            }
            else
            {
                return BadRequest("Failed to create product.");
            }
            
        }
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Models.Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            var prod = this._mapper.Map<Entities.Product>(product);

            prod.ModifiedIP = "2.2.2.2";

            var result = this._productsRepository.Update(id, prod);

            if (result)
            {
                var item = this._productsRepository.Find(id);
                return Ok(item);
            }
            else
            {
                return BadRequest("Failed to Update product.");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(string id, string sort="id")
        {
            var product = this._productsRepository.Delete(id);
            
            if (product)
            {
                var list = this._productsRepository.GetAll(sort);
                if (!list.Any())
                {
                    return NotFound();
                }

                var models = this._mapper.Map<IEnumerable<Models.Product>>(list);
                return Ok(models);
            }
            else
            {
                return BadRequest("Failed to Delete product.");
            }
        }

    }
}
