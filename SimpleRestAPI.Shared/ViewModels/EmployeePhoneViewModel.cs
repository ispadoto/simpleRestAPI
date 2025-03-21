using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRestAPI.Shared.ViewModels
{
    public class EmployeePhoneViewModel
    {
        public string? Id { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneType { get; set; }
        public string? EmployeeId { get; set; }
    }
}
