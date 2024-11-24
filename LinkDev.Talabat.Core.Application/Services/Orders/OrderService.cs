using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Orders;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Orders;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Products;
using LinkDev.Talabat.Core.Application.Exceptions;
using LinkDev.Talabat.Core.Domain.Contracts.Persistance;
using LinkDev.Talabat.Core.Domain.Entities.Orders;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Core.Domain.Specifications.Orders;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Services.Orders
{
    internal class OrderService(IBasketService basketService  , IUnitOfWork unitOfWork , IMapper mapper) : IOrderService
    {
        public async Task<OrderToReturnDto> CreateOrderAsync(string buyerEmail, OrderToCreateDto order)
        {
            // 1.Get Basket From Basket Repo

            var basket = await basketService.GetCustomerBasketAsync(order.BasketId);

           

            // 2.Get Selected Items at Basket From Products Repo

            var orderItems = new List<OrderItem>();  

            if(basket.Items.Count() > 0)
            {
                var productRepo = unitOfWork.GetRepository<Product, int>();
                foreach (var item in basket.Items)
                {
                    var product = await productRepo.GetAsync(item.Id);

                    if(product is not null)
                    {
                        var productItemOrdered = new ProductItemOrder()
                        {
                            ProductId = product.Id,
                            ProductName = product.Name,
                            PictureUrl = product.PictureUrl ?? ""
                        };
                        var orderItem = new OrderItem()
                        {
                            Product = productItemOrdered,
                            Price = product.Price,
                            Quantity = item.Quantity,

                            CreatedBy = "1",// To Be Changed After Modifing The Interceptor
                            LastModifiedBy = "1",// To Be Changed After Modifing The Interceptor
                        };

                        orderItems.Add(orderItem);
                    }

                   

                }
            }


            // 3.Calculate SubTotal

            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);

            // 4.Mapping Address

            var address = mapper.Map<Address>(order.ShippingAddress);



            // 5.Get Delivery Method

            var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAsync(order.DeliveryMethodId);

            // 6.Create Order


            var orderToCreate = new Order()
            {
                BuyerEmail = buyerEmail,
                ShippingAddress = address,
                Items = orderItems,
                SubTotal = subTotal,
                DeliveryMethod = deliveryMethod,

                CreatedBy = "1",// To Be Changed After Modifing The Interceptor
                LastModifiedBy = "1",// To Be Changed After Modifing The Interceptor

            };

            await unitOfWork.GetRepository<Order , int>().AddAsync(orderToCreate);
           
            // 7.Save To Database

            var created =await unitOfWork.CompleteAsync() > 0;

            if (!created) throw new BadRequestException("an error has occured during Creating The Order");


            return mapper.Map<OrderToReturnDto>(orderToCreate);


        }

        public async Task<OrderToReturnDto> CreateOrderByIdAsync(string buyerEmail, int orderId)
        {
            var orderSpecs = new OrderSpecifications(buyerEmail, orderId);

            var order = await unitOfWork.GetRepository<Order, int>().GetWithSpecAsync(orderSpecs);

            if (order is null) throw new NotFoundException(nameof(Order), orderId);

            return mapper.Map<OrderToReturnDto>(order);
        }

        public async Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodAsync()
        {
            var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod , int>().GetAllAsync();
            return mapper.Map<IEnumerable<DeliveryMethodDto>>(deliveryMethod);
        }

        public async Task<IEnumerable<OrderToReturnDto>> GetOrdersForUserAsync(string buyerEmail)
        {
            var orderSpecs = new OrderSpecifications(buyerEmail);

            var orders = await unitOfWork.GetRepository<Order , int>().GetAllWithSpecAsync(orderSpecs);
        
            return mapper.Map<IEnumerable<OrderToReturnDto>>(orders);
        }
    }
}
