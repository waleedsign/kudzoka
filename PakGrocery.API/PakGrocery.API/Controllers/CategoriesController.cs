using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PakGrocery.API.Helpers;
using PakGrocery.API.Repositories.Contracts;

namespace PakGrocery.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IMapper _mapper;
        public CategoriesController(ICategoriesRepository categoriesRepository, IMapper mapper)
        {
            this._categoriesRepository = categoriesRepository;
            this._mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Models.Category>> Get(string sort = "id", int pageNumber = 1, int pageSize = 10)
        {
            var list = this._categoriesRepository.GetAll(sort);
            if (!list.Any())
            {
                return NotFound();
            }
            var models = this._mapper.Map<IEnumerable<Models.Category>>(list);
            return Ok(models);
        }
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Models.Category>> Get(string id)
        {
            var category = this._categoriesRepository.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            var model = this._mapper.Map<Models.Category>(category);
            return Ok(model);
        }
        [HttpPost]
        public ActionResult Post([FromBody]Models.Category category)
        {
            if (category == null)
            {
                return BadRequest();
            }
            var cat = this._mapper.Map<Entities.Category>(category);
            cat.CreatedIP = "21.21.12.12";
            var result = this._categoriesRepository.Insert(cat);
            if (result != null)
            {
                return Created("api/categories", result.Id);
            }
            else
            {
                return BadRequest("Failed to create category.");
            }
        }
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody]Models.Category category)
        {
            if (category == null)
            {
                return BadRequest();
            }
            var cat = this._mapper.Map<Entities.Category>(category);
            cat.ModifiedIP = "12.32.34.12";
            var result = this._categoriesRepository.Update(id, cat);
            if (result)
            {
                var item = this._categoriesRepository.Find(id);
                return Ok(item);
            }
            else
            {
                return BadRequest("Failed to update category.");
            }
        }
        [HttpDelete("{id}")]
        public ActionResult Delete (string id, string sort="id")
        {
            var category = this._categoriesRepository.Delete(id);
            if (category)
            {
                var list = this._categoriesRepository.GetAll(sort);
                if (!list.Any())
                {
                    return NotFound();
                }
                var models = this._mapper.Map<IEnumerable<Models.Category>>(list);
                return Ok(models);
            }
            else
            {
                return BadRequest("Failed to delete category.");
            }
        }
    }
}