using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPI.Data;
using RestAPI.Data.models;

namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _db;
        public CategoriesController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var cats = await _db.categories.ToListAsync();
            return Ok(cats);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategories(int id)
        {
            var c = await _db.categories.SingleOrDefaultAsync(x => x.Id == id);
            if (c == null)
            {
                return NotFound($"Category Id {id} Not Exist");
            }
            return Ok(c);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(string category)
        {
            Category c = new() { Name = category };
            await _db.categories.AddAsync(c);
            _db.SaveChanges();
            return Ok(c);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] Category category)
        {
            var c = await _db.categories.SingleOrDefaultAsync(x => x.Id == category.Id);
            if (c == null)
            {
                return NotFound($"Category Id {category.Id} Not Exist");
            }
            c.Name = category.Name;
            _db.SaveChanges();
            return Ok(c);    
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCategory([FromBody] JsonPatchDocument<Category> category, [FromRoute] int id)
        {
            var c = await _db.categories.SingleOrDefaultAsync(x => x.Id == id);
            if (c == null)
            {
                return NotFound($"Category Id {id} Not Exist");
            }
            category.ApplyTo(c);    
            await _db.SaveChangesAsync();  
            return Ok(c);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCategory(int id)
        {
            var c = await _db.categories.SingleOrDefaultAsync(x => x.Id == id);
            if (c == null)
            {
                return NotFound($"Category Id {id} Not Exist");
            }
            _db.categories.Remove(c);
            _db.SaveChanges();
            return Ok(c);
        }

    }
}
