using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_PJ01_Domain.Exceptions.NotFound
{
    public class DeliveryMethodNotFound(int id) : NotFoundException ($"Delivery Method with id '{id}' was not found.")
    {
    }
}
