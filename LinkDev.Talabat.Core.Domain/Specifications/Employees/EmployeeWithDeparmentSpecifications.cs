using LinkDev.Talabat.Core.Domain.Entities.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Specifications.Employees
{
    public class EmployeeWithDeparmentSpecifications : BaseSpecifications<Employee , int>
    {
        public EmployeeWithDeparmentSpecifications() : base() 
        {
            Includes.Add(E => E.Department!);
        }
        public EmployeeWithDeparmentSpecifications(int id) : base(id)
        {
            Includes.Add(E => E.Department!);
        }
    }

}
