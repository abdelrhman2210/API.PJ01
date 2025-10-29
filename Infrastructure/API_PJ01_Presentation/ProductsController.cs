using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_PJ01_Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace API_PJ01_Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpGet] // GET : baseURL/api/products
        public async Task<IActionResult> GetAllProductsAsync(int? brandId, int? typeId)
        {
            var result = await _serviceManager.ProductService.GetAllProductsAsync(brandId, typeId);
            if (result is null) return BadRequest();
            return Ok(result);
        }

        [HttpGet("{id}")] // GET : baseURL/api/products/1
        public async Task<ActionResult> GetProductById(int? id)
        {
            if (id is null) return BadRequest();
            var result = await _serviceManager.ProductService.GetProductByIdAsync(id.Value);
            if (result is null) return NotFound(); // 404
            return Ok(result); // 200
        }

        [HttpGet(template: "brands")] // GET: baseUrl/api/products/brands
        public async Task<IActionResult> GetAllBrands()
        {
            var result = await _serviceManager.ProductService.GetAllBrandsAsync();
            if (result is null) return BadRequest(); // 400
            return Ok(result); // 200
        }

        [HttpGet(template: "types")] // GET: baseUrl/api/products/types
        public async Task<IActionResult> GetAllTypes()
        {
            var result = await _serviceManager.ProductService.GetAllTypesAsync();
            if (result is null) return BadRequest(); // 400
            return Ok(result); // 200
        }
    }
}
