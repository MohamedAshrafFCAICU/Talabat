using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Basket;
using LinkDev.Talabat.Core.Application.Exceptions;
using LinkDev.Talabat.Core.Domain.Contracts.Infrastructure;
using LinkDev.Talabat.Core.Domain.Entities.Basket;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Services.Basket
{
    internal class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public BasketService(IBasketRepository basketRepository , IMapper mapper , IConfiguration configuration)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<CustomerBasketDto> GetCustomerBasketAsync(string basketId)
        {
            var basket = await _basketRepository.GetAsync(basketId);

            if (basket is null) throw new NotFoundException(nameof(CustomerBasket), basketId);
        
            return _mapper.Map<CustomerBasketDto>(basket);  
        }
        public async Task<CustomerBasketDto> UpdateCustomerBasketAsync(CustomerBasketDto customerBasket)
        {
            var mappedBasket = _mapper.Map<CustomerBasket>(customerBasket);

            var timeToLive = TimeSpan.FromDays(double.Parse(_configuration.GetSection("RedisSettings")["TimeToLiveInDays"]!));  

            var updatedBasket = await _basketRepository.UpdateAsync(mappedBasket , timeToLive);

            if (updatedBasket is null) 
                throw new BadRequestException("can't update  there is a Problem With your Basket");

            return customerBasket;
        }
        public async Task DeleteCustomerBasketAsync(string basketId)
        {
           var deleted =  await _basketRepository.DeleteAsync(basketId);

            if (!deleted) throw new BadRequestException("Un able To delete The Basket");

        }
    }
}
