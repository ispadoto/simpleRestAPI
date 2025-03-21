using SimpleRestAPI.Domain.Entities.Employees;
using SimpleRestAPI.Domain.Entities.EmployeesPhones;
using SimpleRestAPI.Shared.Extensions;
using SimpleRestAPI.Shared.Utils;
using SimpleRestAPI.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRestAPI.Application.EmployeesApplication
{
    public class EmployeeApplication : ApplicationBase<Employee>, IEmployeeApplication
    {
        private readonly IEmployeeService _employeeService;
        private readonly IEmployeePhoneService _employeePhoneService;
        public EmployeeApplication(IEmployeeService employeeService, IEmployeePhoneService employeePhoneService) : base(employeeService)
        {
            _employeeService = employeeService;
            _employeePhoneService = employeePhoneService;
        }

        public async Task<Guid> Add(EmployeeViewModel employee)
        {
            try
            {
                var docNumberExists = await CheckIfDocNumberExists(employee.DocNumber, null);
                if (docNumberExists)
                    throw new Exception("DocNumber alread exists");

                var password = PasswordUtils.CalculateHash(employee.Password);
                var entity = new Employee
                {
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    DocNumber = employee.DocNumber,
                    BirthDate = employee.BirthDate,
                    Address = employee.Address,
                    City = employee.City,
                    ManagerId = new Guid(employee.ManagerId),
                    RoleId = employee.RoleId,
                    Password = password,
                    ManagerName = employee.ManagerName
                };
                var result = await _employeeService.Add(entity);

                if (result != null && result.HasValue && employee.Phones?.Count > 0)
                {
                    foreach (var phone in employee.Phones)
                    {
                        var phoneEntity = new EmployeePhone
                        {
                            EmployeeId = result.Value,
                            PhoneNumber = phone.PhoneNumber,
                            PhoneType = phone.PhoneType
                        };
                        await _employeePhoneService.Add(phoneEntity);
                    }
                }
                return result.Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Update(EmployeeViewModel employee)
        {
            try
            {
                var docNumberExists = await CheckIfDocNumberExists(employee.DocNumber, new Guid(employee.Id));
                if (docNumberExists)
                    throw new Exception("DocNumber alread exists");

                var entity = _employeeService.GetById(new Guid(employee.Id)).Result;
                var password = PasswordUtils.CalculateHash(employee.Password);

                entity.Id = new Guid(employee.Id);
                entity.FirstName = employee.FirstName;
                entity.LastName = employee.LastName;
                entity.Email = employee.Email;
                entity.DocNumber = employee.DocNumber;
                entity.BirthDate = employee.BirthDate;
                entity.Address = employee.Address;
                entity.City = employee.City;
                entity.ManagerId = new Guid(employee.ManagerId);
                entity.RoleId = employee.RoleId;
                entity.Password = password;

                var result = await _employeeService.Update(entity);
                if (result)
                {
                    await RemovePhones(employee.Id); // sempre que atualizar, remove os telefones e insere novamente
                    if (employee.Phones != null || employee.Phones?.Count > 0)
                    {
                        foreach (var phone in employee.Phones)
                        {
                            var phoneEntity = new EmployeePhone
                            {
                                EmployeeId = new Guid(employee.Id),
                                PhoneNumber = phone.PhoneNumber,
                                PhoneType = phone.PhoneType
                            };
                            await _employeePhoneService.Add(phoneEntity);
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<bool> RemovePhones(string employeeId)
        {
            var phones = await _employeePhoneService.GetByWhere($" WHERE EmployeeId = '{employeeId}'");
            foreach (var phone in phones)
            {
                await _employeePhoneService.Remove(phone.Id);
            }
            return true;
        }

        public async Task<bool> Remove(string guid)
        {
            var phones = await _employeePhoneService.GetByWhere($" WHERE EmployeeId = '{guid}'");
            foreach (var phone in phones)
            {
                await _employeePhoneService.Remove(phone.Id);
            }
            return await _employeeService.Remove(new Guid(guid));
        }

        public async Task<EmployeeViewModel?> GetById(string guid)
        {
            var entity = await _employeeService.GetById(new Guid(guid));
            return entity.Map<EmployeeViewModel>();
        }

        public async Task<IQueryable<EmployeeViewModel>> GetAll()
        {
            var entityList = await _employeeService.GetAll();

            // meu auto mapper nao esta funcionando, farei na mão...
            //return entityList.Map<IQueryable<EmployeeViewModel>>();

            var result = new List<EmployeeViewModel>();

            if (entityList.Count() > 0)
            {
                foreach (var item in entityList)
                {
                    var phones = await _employeePhoneService.GetByWhere($" WHERE EmployeeId = '{item.Id}'");

                    var itemVM = new EmployeeViewModel
                    {
                        Id = item.Id.ToString(),
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        Email = item.Email,
                        DocNumber = item.DocNumber,
                        BirthDate = item.BirthDate,
                        Address = item.Address,
                        City = item.City,
                        ManagerId = item.ManagerId.ToString(),
                        ManagerName = item.ManagerName,
                        RoleId = item.RoleId
                    };
                    if (phones.Count() > 0)
                    {
                        itemVM.Phones = new List<EmployeePhoneViewModel>();
                        foreach (var phon in phones)
                        {
                            itemVM.Phones.Add(new EmployeePhoneViewModel
                            {
                                Id = phon.Id.ToString(),
                                EmployeeId = phon.EmployeeId.ToString(),
                                PhoneNumber = phon.PhoneNumber,
                                PhoneType = phon.PhoneType
                            });
                        }
                    };
                    result.Add(itemVM);
                }
            }
            return result.AsQueryable();
        }

        public async Task<LoginResultViewModel> Login(LoginViewModel login)
        {
            var password = PasswordUtils.CalculateHash(login.Password);
            var entity = await _employeeService.GetByWhere($" WHERE DocNumber = '{login.Login}'");
            if (entity == null || entity.Count() == 0)
                throw new Exception("Usuário não encontrado");

            bool validPassword = PasswordUtils.VerifyPassword(login.Password, entity.FirstOrDefault().Password);

            if (!validPassword)
             throw new Exception("Senha inválida");
            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, entity.FirstOrDefault().FirstName),
                new Claim(ClaimTypes.Email, entity.FirstOrDefault().Email),
            };

            var token = TokenService.GenerateToken(entity, claims);


            return new LoginResultViewModel
            {
                Token = token,
                RoleId = entity.FirstOrDefault().RoleId
            };
        }

        private Task<bool> CheckIfDocNumberExists(string docNumber, Guid? employeeId)
        {

            if (employeeId != null && employeeId != Guid.Empty)
            {
                return Task.FromResult(_employeeService.GetByWhere($" WHERE DocNumber = '{docNumber}' AND Id != '{employeeId}'").Result.Count() > 0);
            }
            else
            {
                return Task.FromResult(_employeeService.GetByWhere($" WHERE DocNumber = '{docNumber}'").Result.Count() > 0);
            }
        }

    }
}
