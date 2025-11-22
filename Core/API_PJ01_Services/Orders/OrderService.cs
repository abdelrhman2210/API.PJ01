using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_PJ01_Domain.Contracts;
using API_PJ01_Domain.Entities.Orders;
using API_PJ01_Domain.Entities.Products;
using API_PJ01_Domain.Exceptions.BadRequest;
using API_PJ01_Domain.Exceptions.NotFound;
using API_PJ01_Services.Abstractions.Orders;
using API_PJ01_Services.Specifications.Orders;
using API_PJ01_Shared.Dtos.Orders;
using AutoMapper;

namespace API_PJ01_Services.Orders
{
    public class OrderService(IUnitOfWork _unitOfWork, IMapper _mapper, IBasketRepository basketRepository) : IOrderService
    {
        public async Task<OrderResponse?> CreateOrderAsync(OrderRequest request, string userEmail)
        {
            #region 1- Get Order Address
            var orderAddress = _mapper.Map<OrderAddress>(request.ShipToAddress);
            #endregion

            #region 2- Get Delivery Method By Id
            var deliveryMethod = await _unitOfWork.GetRepository<int, DeliveryMethod>().GetAsync(request.DeliveryMethodId);
            if (deliveryMethod is null) throw new DeliveryMethodNotFound(request.DeliveryMethodId);
            #endregion

            #region 3- Get Order Items
            // 3.1. Get Basket By Id
            var basket = await basketRepository.GetBasketAsync(request.BasketId);
            if (basket is null) throw new BasketNotFoundException(request.BasketId);

            // 3.2. Convert Every Basket Item To Order Item
            var orderItems = new List<OrderItem>();

            foreach (var item in basket.Items)
            {
                // Check Price
                // Get Product From Db
                var product = await _unitOfWork.GetRepository<int, Product>().GetAsync(item.Id);
                if (product is null) throw new ProductNotFoundException(item.Id);

                if (product.Price != item.Price) item.Price = product.Price;

                var productInOrderItem = new ProductInOrderItem(item.Id, item.ProductName, item.PictureUrl);
                var orderItem = new OrderItem(productInOrderItem, item.Price, item.Quantity);
                orderItems.Add(orderItem);
            }
            #endregion

            #region 4- Calculate SubTotal
            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);
            #endregion

            #region Create Order
            var order = new Order(userEmail, orderAddress, deliveryMethod, orderItems, subTotal);
            #endregion

            #region Add Order In DB
            await _unitOfWork.GetRepository<Guid, Order>().AddAsync(order);
            var count = await _unitOfWork.SaveChangesAsync();
            if (count <= 0)
            {
                throw new CreateOrderBadRequestException();
            }
            return _mapper.Map<OrderResponse>(order); 
            #endregion
        }

        public async Task<IEnumerable<DeliveryMethodResponse>> GetAllDeliveryMethodsAsync()
        {
            var deliveryMethods = await _unitOfWork.GetRepository<int, DeliveryMethod>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethodResponse>>(deliveryMethods);
        }

        public async Task<OrderResponse?> GetOrderByIdForSpecificUserAsync(Guid id, string UserEmail)
        {
            var specification = new OrderSpecification(id, UserEmail);
            var order = await _unitOfWork.GetRepository<Guid, Order>().GetAsync(specification);
            return _mapper.Map<OrderResponse>(order); 
        }

        public async Task<IEnumerable<OrderResponse>> GetOrdersForSpecificUserAsync(string UserEmail)
        {
            var specification = new OrderSpecification(UserEmail);
            var order = await _unitOfWork.GetRepository<Guid, Order>().GetAllAsync(specification);
            return _mapper.Map<IEnumerable<OrderResponse>>(order);
        }
    }
}
