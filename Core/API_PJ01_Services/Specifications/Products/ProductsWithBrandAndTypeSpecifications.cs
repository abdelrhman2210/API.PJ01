using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using API_PJ01_Domain.Entities.Products;
using API_PJ01_Shared.Dtos.Products;

namespace API_PJ01_Services.Specifications.Products
{
    public class ProductsWithBrandAndTypeSpecifications : BaseSpecifications<int, Product>
    {
        public ProductsWithBrandAndTypeSpecifications(int id) : base(P => P.Id == id)
        {
            AddIncludes();
        }

        public ProductsWithBrandAndTypeSpecifications(ProductQueryParameters parameters) : base
            (
                p => 
                (!parameters.brandId.HasValue || p.BrandId == parameters.brandId)
                &&
                (!parameters.typeId.HasValue || p.TypeId == parameters.typeId)
                &&
                (string.IsNullOrEmpty(parameters.search) || p.Name.Contains(parameters.search.ToLower()))
            )
        {
            
            ApplyPagination(parameters.pageSize, parameters.pageIndex);
            ApplySorting(parameters.sort);
            AddIncludes();
        }

        private void ApplySorting(string? sort)
        {
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort.ToLower())
                {
                    case "priceasc":
                        //OrderBy = P => P.Price;
                        AddOrderBy(P => P.Price);
                        break;
                    case "pricedesc":
                        //OrderByDescending = P => P.Price;
                        AddOrderByDescending(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
            }
            else
            {
                //OrderBy = P => P.Name;
                AddOrderBy(P => P.Name);
            }
        }

        private void AddIncludes()
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Type);
        }
    }
}
