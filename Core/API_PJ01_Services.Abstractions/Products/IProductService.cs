using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_PJ01_Shared.Dtos.Products;

namespace API_PJ01_Services.Abstractions.Products
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponse>> GetAllProductsAsync(int? brandId, int? typeId , string? sort, string? search, int? pageIndex, int? pageSize);
        Task<ProductResponse> GetProductByIdAsync(int id);
        Task<IEnumerable<BrandTypeResponse>> GetAllBrandsAsync();
        Task<IEnumerable<BrandTypeResponse>> GetAllTypesAsync();
    }
}
