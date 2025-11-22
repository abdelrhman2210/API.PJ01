using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_PJ01_Shared.Dtos.Orders;

namespace API_PJ01_Services.Abstractions.Orders
{
    public interface IOrderService
    {
        Task<OrderResponse?> CreateOrderAsync(OrderRequest request, string userEmail);
        Task<IEnumerable<DeliveryMethodResponse>> GetAllDeliveryMethodsAsync();
        Task<OrderResponse?> GetOrderByIdForSpecificUserAsync(Guid id, string UserEmail);
        Task<IEnumerable<OrderResponse>> GetOrdersForSpecificUserAsync(string UserEmail);
    }
}
