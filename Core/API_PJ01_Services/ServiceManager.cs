using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_PJ01_Domain.Contracts;
using API_PJ01_Services.Abstractions;
using API_PJ01_Services.Abstractions.Baskets;
using API_PJ01_Services.Abstractions.Products;
using API_PJ01_Services.Baskets;
using API_PJ01_Services.Products;
using AutoMapper;

namespace API_PJ01_Services
{
    public class ServiceManager(
        IUnitOfWork _unitOfWork, 
        IMapper _mapper,
        IBasketRepository _basketRepository
        ) : IServiceManager
    {
        public IProductService ProductService { get; } = new ProductService(_unitOfWork , _mapper);

        public IBasketService BasketService { get; } = new BasketService(_basketRepository, _mapper);
    }
}
