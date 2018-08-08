using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YLKnowledgeBase.Data;
using YLKnowledgeBase.Models;
using YLKnowledgeBase.Services;

namespace YLKnowledgeBase.Controllers
{
    [Produces("application/json")]
    [Route("api/Categories")]
    public class APICategoriesController : Controller
    {
        //private readonly ApplicationDbContext _context;
        private readonly ICategoryService _categoryService;
        public APICategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task <IEnumerable<Category>> GetCategories()
        {
            return await _categoryService.GetAllCategories();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _categoryService.GetCategory(id);
            //var category = await _context.Categories.SingleOrDefaultAsync(m => m.CategoryId == id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // PUT: api/Categories/5
        [HttpPut("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> PutCategory([FromRoute] Guid id, [FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != category.CategoryId)
            {
                return BadRequest();
            }

            _categoryService.EditCategory(category);
            //_context.Entry(category).State = EntityState.Modified;

            try
            {
                await _categoryService.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Categories
        [HttpPost]
        public async Task<IActionResult> PostCategory([FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _categoryService.CreateCategory(category);
            await _categoryService.Save();

            return CreatedAtAction("GetCategory", new { id = category.CategoryId }, category);
        }

        /*
        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _context.Categories.SingleOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return Ok(category);
        }
        */

        private bool CategoryExists(Guid id)
        {
           return _categoryService.CategoryExists(id);
           //return _context.Categories.Any(e => e.CategoryId == id);
        }
    }
}