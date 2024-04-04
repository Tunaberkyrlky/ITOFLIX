using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ITOFLIX.Data;
using ITOFLIX.Models;
using Microsoft.AspNetCore.Authorization;

namespace ITOFLIX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ITOFLIXContext _context;

        public CategoriesController(ITOFLIXContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        [Authorize]
        public ActionResult<List<Category>> GetCategories()
        {
            return  _context.Categories.AsNoTracking().ToList();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<Category> GetCategory(short id)
        {
            Category? category = _context.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "ContentAdmin")]
        public ActionResult PutCategory(Category category)
        {
            _context.Categories.Update(category);

            try
            {
                 _context.SaveChanges();
            }
            catch (Exception ex)
            {
            }

            return NoContent();
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "ContentAdmin")]
        public  ActionResult<short> PostCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();

            return Ok(category.Id);
        }
    }
}
