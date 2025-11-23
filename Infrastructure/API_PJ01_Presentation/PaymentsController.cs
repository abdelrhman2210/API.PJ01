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
    public class PaymentsController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpPost("{basketId}")]
        public async Task<IActionResult> CreatePaymentIntent(string basketId)
        {
            var result = await _serviceManager.PaymentService.CreatePaymentIntentAsync(basketId);
            return Ok(result);
        }
    }
}
