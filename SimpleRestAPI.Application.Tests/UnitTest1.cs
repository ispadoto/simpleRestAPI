using Moq;
using SimpleRestAPI.Application.EmployeesApplication;
using SimpleRestAPI.Domain.Entities.Employees;
using SimpleRestAPI.Domain.Entities.EmployeesPhones;
using SimpleRestAPI.Shared.Utils;
using SimpleRestAPI.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SimpleRestAPI.Application.Tests
{
    public class EmployeeApplicationTests
    {
        private readonly Mock<IEmployeeService> _mockEmployeeService;
        private readonly Mock<IEmployeePhoneService> _mockEmployeePhoneService;
        private readonly EmployeeApplication _employeeApplication;

        public EmployeeApplicationTests()
        {
            _mockEmployeeService = new Mock<IEmployeeService>();
            _mockEmployeePhoneService = new Mock<IEmployeePhoneService>();
            _employeeApplication = new EmployeeApplication(_mockEmployeeService.Object, _mockEmployeePhoneService.Object);
        }

        [Fact]
        public async Task Add_ShouldReturnGuid_WhenEmployeeIsAdded()
        {
            // Arrange
            var employeeViewModel = new EmployeeViewModel
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                DocNumber = "123456789",
                BirthDate = new DateTime(1990, 1, 1),
                Address = "123 Main St",
                City = "Anytown",
                ManagerId = Guid.NewGuid().ToString(),
                RoleId = 1,
                Password = "password",
                ManagerName = "Manager",
                Phones = new List<EmployeePhoneViewModel>
                {
                    new EmployeePhoneViewModel { PhoneNumber = "123-456-7890", PhoneType = "Mobile" }
                }
            };

            _mockEmployeeService.Setup(s => s.Add(It.IsAny<Employee>())).ReturnsAsync(Guid.NewGuid());

            // Act
            var result = await _employeeApplication.Add(employeeViewModel);

            // Assert
            Assert.NotEqual(Guid.Empty, result);
        }

        [Fact]
        public async Task Update_ShouldReturnTrue_WhenEmployeeIsUpdated()
        {
            // Arrange
            var employeeViewModel = new EmployeeViewModel
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                DocNumber = "123456789",
                BirthDate = new DateTime(1990, 1, 1),
                Address = "123 Main St",
                City = "Anytown",
                ManagerId = Guid.NewGuid().ToString(),
                RoleId = 1,
                Password = "password",
                Phones = new List<EmployeePhoneViewModel>
                {
                    new EmployeePhoneViewModel { PhoneNumber = "123-456-7890", PhoneType = "Mobile" }
                }
            };

            _mockEmployeeService.Setup(s => s.GetById(It.IsAny<Guid>())).ReturnsAsync(new Employee());
            _mockEmployeeService.Setup(s => s.Update(It.IsAny<Employee>())).ReturnsAsync(true);

            // Act
            var result = await _employeeApplication.Update(employeeViewModel);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Remove_ShouldReturnTrue_WhenEmployeeIsRemoved()
        {
            // Arrange
            var guid = Guid.NewGuid().ToString();
            _mockEmployeeService.Setup(s => s.Remove(It.IsAny<Guid>())).ReturnsAsync(true);

            // Act
            var result = await _employeeApplication.Remove(guid);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetById_ShouldReturnEmployeeViewModel_WhenEmployeeExists()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var employee = new Employee { Id = guid, FirstName = "John", LastName = "Doe" };
            _mockEmployeeService.Setup(s => s.GetById(It.IsAny<Guid>())).ReturnsAsync(employee);

            // Act
            var result = await _employeeApplication.GetById(guid.ToString());

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John", result.FirstName);
            Assert.Equal("Doe", result.LastName);
        }

        [Fact]
        public async Task GetAll_ShouldReturnEmployeeViewModels_WhenEmployeesExist()
        {
            // Arrange
            var employees = new List<Employee>
            {
                new Employee { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe" },
                new Employee { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Doe" }
            }.AsQueryable();

            _mockEmployeeService.Setup(s => s.GetAll()).ReturnsAsync(employees);

            // Act
            var result = await _employeeApplication.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task Login_ShouldReturnLoginResultViewModel_WhenLoginIsSuccessful()
        {
            // O login é feito com base no DocNumber, que é o login do usuário, ele irá falhar por conta do tokenKey depender ser pego do projeto de API e não do projeto de aplicação...
            // Arrange
            var loginViewModel = new LoginViewModel { Login = "admin", Password = "123456" };
            var employees = new List<Employee>
            {
                new Employee { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", DocNumber = "admin", Password = PasswordUtils.CalculateHash("123456"), Email = "ibere@testes.com", RoleId = 1 }
            }.AsQueryable();

            _mockEmployeeService.Setup(s => s.GetByWhere(It.IsAny<string>())).ReturnsAsync(employees);

            // Act
            var result = await _employeeApplication.Login(loginViewModel);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Token);
        }
    }
}
