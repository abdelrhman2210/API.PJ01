using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_PJ01_Domain.Exceptions.NotFound
{
    public class BasketNotFoundException(string id) :
        NotFoundException( $"Basket with Key {id} Was Not Found!")
    {
    }
}
