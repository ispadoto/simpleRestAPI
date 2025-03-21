using SimpleRestAPI.Domain.Entities.EmployeesPhones;
using SimpleRestAPI.Infra.Database.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRestAPI.Infra.Database.Repositories
{
    public class EmployeePhoneRepository : RepositoryBase<EmployeePhone>, IEmployeePhoneRepository
    {
        public EmployeePhoneRepository(SimpleRestDB context) : base(context)
        {
        }
    }
}
