using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_PJ01_Shared.Dtos.Products
{
    public class ProductQueryParameters
    {
        public int? brandId { get; set; }
        public int? typeId { get; set; }
        public string? sort { get; set; }
        public string? search { get; set; }
        public int pageSize { get; set; } = 5;
        public int pageIndex { get; set; } = 1;
    }
}
