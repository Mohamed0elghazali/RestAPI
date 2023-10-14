using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPI.Data;
using RestAPI.Data.models;
using RestAPI.Migrations;
using RestAPI.Models;

namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ItemsController(AppDbContext db)
        {
            _db = db;   
        }

        [HttpGet]
        public async Task<IActionResult> AllItems()
        {
            var items = await _db.items.ToListAsync();
            return Ok(items);   
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> AllItems(int id)
        {
            var item = await _db.items.SingleOrDefaultAsync(x => x.Id == id);
            if (item == null)
            {
                return NotFound($"Item Id {id} Doesn`t Exist");

            }
            return Ok(item);
        }


        [HttpGet("CategoryID/{idcategory}")]
        public async Task<IActionResult> AllItemsWithCategoryID(int idcategory)
        {
            var item = await _db.items.Where(x => x.CategoryID == idcategory).ToListAsync();
            if (item == null)
            {
                return NotFound($"Category Id {idcategory} has no items");

            }
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> AddItem([FromForm] mdItem mdl)
        {
            using var steam = new MemoryStream();
            await mdl.Image.CopyToAsync(steam);

            var item = new Item
            {
                Name = mdl.Name,
                Price = mdl.Price,
                Notes = mdl.Notes,
                CategoryID = mdl.CategoryID,
                Image = steam.ToArray(),
            };
            await _db.items.AddAsync(item);
            await _db.SaveChangesAsync();
            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _db.items.SingleOrDefaultAsync(x => x.Id == id);
            if (item == null)
            {
                return NotFound($"item id {id} doesn`t exist ");
            }   
            _db.items.Remove(item);
            await _db.SaveChangesAsync();
            return Ok(item);    
        }


    }

}
