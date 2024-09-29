using Microsoft.AspNetCore.Mvc;
using StoreSampleAPI.DTOs;
using StoreSampleAPI.Models;
using StoreSampleAPI.Services;

namespace StoreSampleAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts()
        {
            var products = await _productService.GetAllProducts();
            return Ok(products);
        }
    }
}
