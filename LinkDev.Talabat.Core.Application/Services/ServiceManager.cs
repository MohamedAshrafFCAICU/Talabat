using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Services;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Auth;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Employees;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Products;
using LinkDev.Talabat.Core.Application.Services.Employees;
using LinkDev.Talabat.Core.Application.Services.Products;
using LinkDev.Talabat.Core.Domain.Contracts.Persistance;
using Microsoft.Extensions.Configuration;

namespace LinkDev.Talabat.Core.Application.Services
{
    internal class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> productService;
        private readonly Lazy<IEmployeeService> employeeService;
        private readonly Lazy<IBasketService> basketService;
        private readonly Lazy<IAuthService> authService;

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public ServiceManager(IUnitOfWork unitOfWork , IMapper mapper , IConfiguration configuration , Func<IBasketService> basketServiceFactory , Func<IAuthService> authSerivceFactory)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.configuration = configuration;
            
            productService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
            employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(unitOfWork, mapper));
            basketService = new Lazy<IBasketService>(basketServiceFactory); 
            authService = new Lazy<IAuthService>(authSerivceFactory , LazyThreadSafetyMode.ExecutionAndPublication);
        }

        public IProductService ProductService  => productService.Value;

        public IEmployeeService EmployeeService => employeeService.Value;

        public IBasketService BasketService => basketService.Value;

        public IAuthService AuthService => authService.Value;
    }
}
