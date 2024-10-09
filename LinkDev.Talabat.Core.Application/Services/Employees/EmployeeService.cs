using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Employees;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Employees;
using LinkDev.Talabat.Core.Domain.Contracts.Persistance;
using LinkDev.Talabat.Core.Domain.Entities.Employees;
using LinkDev.Talabat.Core.Domain.Specifications;
using LinkDev.Talabat.Core.Domain.Specifications.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Services.Employees
{
    internal class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public EmployeeService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<EmployeeToReturnDto> GetEmployeeAsync(int id)
        {
            var spec = new EmployeeWithDeparmentSpecifications(id);

            var Employee = await unitOfWork.GetRepository<Employee, int>().GetWithSpecAsync(spec);

            var employee = mapper.Map<EmployeeToReturnDto>(Employee);

            return employee;
        }

        public async Task<IEnumerable<EmployeeToReturnDto>> GetEmployeesAsync()
        {
            var spec = new EmployeeWithDeparmentSpecifications();   

           var Employees =  await unitOfWork.GetRepository<Employee, int>().GetAllWithSpecAsync(spec);

            var employees = mapper.Map<IEnumerable<EmployeeToReturnDto>>(Employees);

            return employees;
        }
    }
}
