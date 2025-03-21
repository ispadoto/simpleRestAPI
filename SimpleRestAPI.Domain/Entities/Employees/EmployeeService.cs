
namespace SimpleRestAPI.Domain.Entities.Employees
{
    public class EmployeeService : ServiceBase<Employee>, IEmployeeService
    {
        private readonly IEmployeeRepository _IEmployeeRepository;

        public EmployeeService(IEmployeeRepository _EmployeeRepository) : base(_EmployeeRepository)
        {
            _IEmployeeRepository = _EmployeeRepository;
        }
    }
}
