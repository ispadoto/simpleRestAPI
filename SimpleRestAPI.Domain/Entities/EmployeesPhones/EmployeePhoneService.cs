
namespace SimpleRestAPI.Domain.Entities.EmployeesPhones
{
    public class EmployeePhoneService : ServiceBase<EmployeePhone>, IEmployeePhoneService
    {
        private readonly IEmployeePhoneRepository _IEmployeePhoneRepository;

        public EmployeePhoneService(IEmployeePhoneRepository _EmployeePhoneRepository) : base(_EmployeePhoneRepository)
        {
            _IEmployeePhoneRepository = _EmployeePhoneRepository;
        }
    }
}
