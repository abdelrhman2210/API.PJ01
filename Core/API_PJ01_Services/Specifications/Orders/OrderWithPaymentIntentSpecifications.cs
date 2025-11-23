using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_PJ01_Domain.Entities.Orders;

namespace API_PJ01_Services.Specifications.Orders
{
    public class OrderWithPaymentIntentSpecifications : BaseSpecifications<Guid, Order>
    {
        public OrderWithPaymentIntentSpecifications(string paymentIntentId) : base(O => O.PaymentIntentId == paymentIntentId)
        {
        }
    }
}
