using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_PJ01_Domain.Contracts;
using API_PJ01_Domain.Entities.Identity;
using API_PJ01_Services.Abstractions;
using API_PJ01_Services.Abstractions.Auth;
using API_PJ01_Services.Abstractions.Baskets;
using API_PJ01_Services.Abstractions.Cache;
using API_PJ01_Services.Abstractions.Orders;
using API_PJ01_Services.Abstractions.Payment;
using API_PJ01_Services.Abstractions.Products;
using API_PJ01_Services.Auth;
using API_PJ01_Services.Baskets;
using API_PJ01_Services.Cache;
using API_PJ01_Services.Orders;
using API_PJ01_Services.Payment;
using API_PJ01_Services.Products;
using API_PJ01_Shared;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace API_PJ01_Services
{
    public class ServiceManager(
        IUnitOfWork _unitOfWork, 
        IMapper _mapper,
        IBasketRepository _basketRepository,
        ICacheRepository _cacheRepository,
        UserManager<AppUser> _userManager,
        IOptions<JwtOptions> options,
        IConfiguration configuration
        ) : IServiceManager
    {
        public IProductService ProductService { get; } = new ProductService(_unitOfWork , _mapper);

        public IBasketService BasketService { get; } = new BasketService(_basketRepository, _mapper);

        public ICacheService CacheService { get; } = new CacheService(_cacheRepository);

        public IAuthService AuthService { get; } = new AuthService(_userManager, options, _mapper);

        public IOrderService OrderService { get; } = new OrderService(_unitOfWork, _mapper, _basketRepository);

        public IPaymentService PaymentService { get; } = new PaymentService(_basketRepository, _unitOfWork, configuration, _mapper);
    }
}
