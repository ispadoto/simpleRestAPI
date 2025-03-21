using SimpleRestAPI.Domain.Entities.Employees;
using SimpleRestAPI.Infra.Database.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRestAPI.Infra.Database.Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(SimpleRestDB context) : base(context)
        {
        }
    }
}
