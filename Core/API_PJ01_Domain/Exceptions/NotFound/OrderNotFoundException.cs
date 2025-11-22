using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_PJ01_Domain.Exceptions.NotFound
{
    public class OrderNotFoundException(Guid id) : NotFoundException($"Order With Id {id} Not Found.")
    {
    }
}
