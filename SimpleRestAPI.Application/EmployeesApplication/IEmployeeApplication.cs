using SimpleRestAPI.Domain.Entities.Employees;
using SimpleRestAPI.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SimpleRestAPI.Application.EmployeesApplication
{
    public interface IEmployeeApplication: IApplicationBase<Employee>
    {
        Task<Guid> Add(EmployeeViewModel employee);
        Task<bool> Update(EmployeeViewModel employee);
        Task<bool> Remove(string guid);
        Task<EmployeeViewModel?> GetById(string guid);
        Task<IQueryable<EmployeeViewModel>> GetAll();
        Task<LoginResultViewModel> Login(LoginViewModel login);
    }
}
