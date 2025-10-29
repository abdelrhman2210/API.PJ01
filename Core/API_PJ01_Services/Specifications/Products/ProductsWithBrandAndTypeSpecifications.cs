using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using API_PJ01_Domain.Entities.Products;

namespace API_PJ01_Services.Specifications.Products
{
    public class ProductsWithBrandAndTypeSpecifications : BaseSpecifications<int, Product>
    {
        public ProductsWithBrandAndTypeSpecifications(int id) : base(P => P.Id == id)
        {
            AddIncludes();
        }

        public ProductsWithBrandAndTypeSpecifications(int? brandId, int? typeId) : base
            (
                p => 
                (!brandId.HasValue || p.BrandId == brandId)
                &&
                (!typeId.HasValue || p.TypeId == typeId)
            )
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Type);
        }
    }
}
