using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_PJ01_Domain.Entities.Orders;

namespace API_PJ01_Services.Specifications.Orders
{
    public class OrderSpecification : BaseSpecifications<Guid, Order>
    {
        public OrderSpecification(Guid id, string userEmail) : base(O => O.Id == id && O.UserEmail.ToLower() == userEmail.ToLower())
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
        }

        public OrderSpecification(string userEmail) : base(O => O.UserEmail.ToLower() == userEmail.ToLower())
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
            AddOrderByDescending(O => O.OrderDate);
        }
    }
}
