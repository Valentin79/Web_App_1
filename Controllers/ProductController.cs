using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Web_API.Models;

namespace Web_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController: ControllerBase
    {
        
        
            [HttpGet("getProduct")]
            public IActionResult GetProducts()
            {
                try
                {
                    using (var context = new ProductContext())
                    {
                        var products = context.Products.Select(x => new Product()
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Description = x.Description
                        });
                        return Ok(products);
                    }
                }
                catch
                {
                    return StatusCode(500);
                }
            }

        [HttpPost("postProduct")]
        public IActionResult PostProducts([FromQuery] string name, string description, int categoryID, int coast)
        {
            try
            {
                using (var context = new ProductContext())
                {
                    if (!context.Products.Any(x => x.Name.ToLower().Equals(name)))
                    {
                        context.Add(new Product()
                        {
                            Name = name,
                            Description = description,
                            Coast = coast,
                            CategoryID = categoryID
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

        [HttpDelete("DeleteProduct")]
        public IActionResult DeleteProducts(int key)
        {
            try
            {
                using (var context = new ProductContext())
                {
                    if (context.Products.Any(x => x.Id == key))
                    {
                        var product = context.Products.Where(x => x.Id == key);
                        context.Remove(product);
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

        [HttpPatch("PathCoastProduct")]
        public IActionResult PathCoastProduct(string name, int coast)
        {
            try
            {
                using (var context = new ProductContext())
                {
                    
                    if (context.Products.Any(x => x.Name.ToLower().Equals(name)))
                    {
                        var product = context.Products.FirstOrDefault(x => x.Name.ToLower().Equals(name));
                        product.Coast = coast;
                        context.SaveChanges();
                        return Ok();
                    }
                    else
                    {
                        return StatusCode(404);
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
