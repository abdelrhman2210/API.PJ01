using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_PJ01_Domain.Entities.Products;
using API_PJ01_Shared.Dtos.Products;

namespace API_PJ01_Services.Specifications.Products
{
    public class ProductsCountSpecifications : BaseSpecifications<int, Product>
    {
        public ProductsCountSpecifications(ProductQueryParameters parameters) : base(
            P =>
                (!parameters.brandId.HasValue || P.BrandId == parameters.brandId) &&
                (!parameters.typeId.HasValue || P.TypeId == parameters.typeId) &&
                (string.IsNullOrEmpty(parameters.search) || P.Name.ToLower().Contains(parameters.search.ToLower()))
        )
        { }
    }
}
