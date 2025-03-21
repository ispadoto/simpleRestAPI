

namespace SimpleRestAPI.Domain.Entities.Employees
{
    public class Employee : EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string DocNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string Password { get; set; }
        public Guid? ManagerId { get; set; }
        public string? ManagerName { get; set; }
        public int RoleId { get; set; }
    }
}
