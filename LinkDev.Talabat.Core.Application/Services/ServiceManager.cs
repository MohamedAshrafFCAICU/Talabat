using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Services;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Employees;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Products;
using LinkDev.Talabat.Core.Application.Services.Employees;
using LinkDev.Talabat.Core.Application.Services.Products;
using LinkDev.Talabat.Core.Domain.Contracts.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Services
{
    internal class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> productService;
        private readonly Lazy<IEmployeeService> employeeService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ServiceManager(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;

            productService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
            employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(unitOfWork, mapper));

        }

        public IProductService ProductService  => productService.Value;

        public IEmployeeService EmployeeService => employeeService.Value;
    }
}
