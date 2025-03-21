

using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleRestAPI.Domain.Entities.EmployeesPhones
{
    public class EmployeePhone : EntityBase
    {
        public string PhoneNumber { get; set; }
        public string PhoneType { get; set; }

        [ForeignKey("EmployeeId")]
        public Guid EmployeeId { get; set; }
    }
}
