using Microsoft.AspNetCore.Mvc;
using Web_API.Models;

namespace Web_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        [HttpGet("GetCategorys")]
        public IActionResult GetCategorys()
        {
            try
            {
                using (var context = new ProductContext())
                {
                    var categorys = context.Products.Select(x => new Category()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description
                    });
                    return Ok(categorys);
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("postCategory")]
        public IActionResult PostCategory([FromQuery] string name, string description)
        {
            try
            {
                using (var context = new ProductContext())
                {
                    if (!context.Categories.Any(x => x.Name.ToLower().Equals(name)))
                    {
                        context.Add(new Category()
                        {
                            Name = name,
                            Description = description,
                        });
                        context.SaveChanges();
                        return Ok();
                    }
                    else
                    {
                        return StatusCode(409);
                    }
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut("putCategory")]
        public IActionResult PutCategory([FromQuery] string name, string description)
        {
            try
            {
                using (var context = new ProductContext())
                {
                    if (!context.Categories.Any(x => x.Name.ToLower().Equals(name)))
                    {
                        context.Add(new Category()
                        {
                            Name = name,
                            Description = description,
                        });
                        context.SaveChanges();
                        return Ok();
                    }
                    else
                    {
                        Category category = context.Categories.FirstOrDefault(x => x.Name.ToLower().Equals(name));
                        category.Name = name;
                        category.Description = description;
                        context.SaveChanges();
                        return Ok();
                    }
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("DeleteCategory")]
        public IActionResult DeleteCategory(int key)
        {
            try
            {
                using (var context = new ProductContext())
                {
                    if (context.Categories.Any(x => x.Id == key))
                    {
                        var category = context.Categories.Where(x => x.Id == key);
                        context.Remove(category);
                        return Ok();
                    }
                    else
                    {
                        return StatusCode(409);
                    }
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
