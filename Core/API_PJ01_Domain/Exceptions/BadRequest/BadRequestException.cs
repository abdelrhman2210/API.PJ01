using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_PJ01_Domain.Exceptions.BadRequest
{
    public abstract class BadRequestException(string message) : Exception(message)
    {
    }
}
