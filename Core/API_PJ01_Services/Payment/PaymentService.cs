using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_PJ01_Domain.Contracts;
using API_PJ01_Domain.Entities.Orders;
using API_PJ01_Domain.Entities.Products;
using API_PJ01_Domain.Exceptions.NotFound;
using API_PJ01_Services.Abstractions.Payment;
using API_PJ01_Shared.Dtos.Baskets;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Stripe;
using Product = API_PJ01_Domain.Entities.Products.Product;

namespace API_PJ01_Services.Payment
{
    public class PaymentService(IBasketRepository _basketRepository, IUnitOfWork _unitOfWork, IConfiguration configuration, IMapper _mapper) : IPaymentService
    {
        public async Task<BasketDto> CreatePaymentIntentAsync(string basketId)
        {
            #region Calculate Amount = SubTotal + Delivery Method Cost
            // get basket
            var basket = await _basketRepository.GetBasketAsync(basketId);
            if (basket is null) throw new BasketNotFoundException(basketId);

            // check product and its price
            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.GetRepository<int, Product>().GetAsync(item.Id);
                if (product is null) throw new ProductNotFoundException(item.Id);

                item.Price = product.Price;
            }
            // calculate subTotal
            var subTotal = basket.Items.Sum(I => I.Price * I.Quantity);

            // Get Delivery Method By Id
            if (!basket.DeliveryMethodId.HasValue) throw new DeliveryMethodNotFound(-1);

            var deliveryMethod = await _unitOfWork.GetRepository<int, DeliveryMethod>().GetAsync(basket.DeliveryMethodId.Value);
            if (deliveryMethod is null) throw new DeliveryMethodNotFound(basket.DeliveryMethodId.Value);

            basket.ShippingCost = deliveryMethod.Price;

            var amount = subTotal + deliveryMethod.Price;
            #endregion

            #region Send Amount TO Stripe
            StripeConfiguration.ApiKey = configuration["StripeOptions:SecretKey"];

            PaymentIntentService paymentIntentService = new PaymentIntentService();
            PaymentIntent paymentIntent;

            if (basket.PaymentIntentId is null)
            {
                // Create
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)amount * 100,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>()
                    {
                        "card"
                    }
                };

                paymentIntent = await paymentIntentService.CreateAsync(options);
            }
            else
            {
                // Update
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)amount * 100,
                };

                paymentIntent = await paymentIntentService.UpdateAsync(basket.PaymentIntentId, options);
            }

            basket.PaymentIntentId = paymentIntent.Id;
            basket.ClientSecret = paymentIntent.ClientSecret;

            basket = await _basketRepository.CreateBasketAsync(basket, TimeSpan.FromDays(value: 1));
            return _mapper.Map<BasketDto>(basket);
            #endregion
        }
    }
}
