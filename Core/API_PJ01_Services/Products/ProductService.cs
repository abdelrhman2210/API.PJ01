using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_PJ01_Domain.Contracts;
using API_PJ01_Domain.Entities.Products;
using API_PJ01_Domain.Exceptions.NotFound;
using API_PJ01_Services.Abstractions.Products;
using API_PJ01_Services.Specifications;
using API_PJ01_Services.Specifications.Products;
using API_PJ01_Shared.Dtos.Pagination;
using API_PJ01_Shared.Dtos.Products;
using AutoMapper;

namespace API_PJ01_Services.Products
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {
        public async Task<PaginationResponse<ProductResponse>> GetAllProductsAsync(ProductQueryParameters parameters)
        {
            var spec = new ProductsWithBrandAndTypeSpecifications(parameters);


            var products = await _unitOfWork.GetRepository<int, Product>().GetAllAsync(spec);
            var result = _mapper.Map<IEnumerable<ProductResponse>>(products);

            var pagedSpec = new ProductsCountSpecifications(parameters);
            var totalItems = await _unitOfWork.GetRepository<int, Product>().CountAsync(pagedSpec);

            return new PaginationResponse<ProductResponse>(parameters.pageIndex, parameters.pageSize, totalItems, result);
        }

        public async Task<ProductResponse> GetProductByIdAsync(int id)
        {
            var spec = new ProductsWithBrandAndTypeSpecifications(id);

            var product = await _unitOfWork.GetRepository<int, Product>().GetAsync(id);
            
            if (product is null) throw new ProductNotFoundException(id);
            var result = _mapper.Map<ProductResponse>(product);
            return result;
        }

        public async Task<IEnumerable<BrandTypeResponse>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.GetRepository<int, ProductBrand>().GetAllAsync();
            var result = _mapper.Map<IEnumerable<BrandTypeResponse>>(brands);
            return result;
        }

        public async Task<IEnumerable<BrandTypeResponse>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.GetRepository<int, ProductType>().GetAllAsync();
            var result = _mapper.Map<IEnumerable<BrandTypeResponse>>(types);
            return result;
        }


    }
}
