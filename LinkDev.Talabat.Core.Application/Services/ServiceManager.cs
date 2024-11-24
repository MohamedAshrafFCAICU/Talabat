using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Services;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Auth;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Employees;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Orders;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Products;
using LinkDev.Talabat.Core.Application.Services.Employees;
using LinkDev.Talabat.Core.Application.Services.Products;
using LinkDev.Talabat.Core.Domain.Contracts.Persistance;
using Microsoft.Extensions.Configuration;

namespace LinkDev.Talabat.Core.Application.Services
{
    internal class ServiceManager : IServiceManager
    {
        private readonly Lazy<IOrderService> orderService;
        private readonly Lazy<IProductService> productService;
        private readonly Lazy<IBasketService> basketService;
        private readonly Lazy<IAuthService> authService;

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public ServiceManager(IUnitOfWork unitOfWork , IMapper mapper ,Func<IOrderService> orderServiceFactory , IConfiguration configuration , Func<IBasketService> basketServiceFactory , Func<IAuthService> authSerivceFactory)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.configuration = configuration;
            
            productService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
            orderService = new Lazy<IOrderService>(orderServiceFactory , LazyThreadSafetyMode.ExecutionAndPublication);
            basketService = new Lazy<IBasketService>(basketServiceFactory); 
            authService = new Lazy<IAuthService>(authSerivceFactory , LazyThreadSafetyMode.ExecutionAndPublication);
        }

        public IProductService ProductService  => productService.Value;

        public IBasketService BasketService => basketService.Value;

        public IAuthService AuthService => authService.Value;

        public IOrderService OrderService => orderService.Value; 
    }
}
