using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_PJ01_Services.Abstractions;
using API_PJ01_Shared.Dtos.Baskets;
using Microsoft.AspNetCore.Mvc;

namespace API_PJ01_Presentation
{
    [ApiController]
    [Route(template: "api/[controller]")]
    public class BasketsController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpGet] // GET: baseUrl/api/baskets?id
        public async Task<IActionResult> GetBasketById(string id)
        {
            var result = await _serviceManager.BasketService.GetBasketAsync(id);
            return Ok(result);
        }

        [HttpPost] // POST: baseUrl/api/baskets
        public async Task<IActionResult> CreateOrUpdateBasket(BasketDto dto)
        {
            var result = await _serviceManager.BasketService.CreateBasketAsync(dto, TimeSpan.FromDays(value: 1));
            return Ok(result);
        }

        [HttpDelete] // DELETE: baseUrl/api/baskets?id
        public async Task<IActionResult> DeleteBasketById(string id)
        {
            var result = await _serviceManager.BasketService.DeleteBasketAsync(id);
            return NoContent(); // 204
        }
    }
}
