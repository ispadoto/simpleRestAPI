using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRestAPI.Shared.ViewModels
{
    public class EmployeeViewModel
    {
        public string? Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name = "Doc Number")]
        public string DocNumber { get; set; }


        [Display(Name = "DoB")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Address")]
        public string? Address { get; set; }

        [Display(Name = "City")]
        public string? City { get; set; }

        [Display(Name = "Manager")]
        public string ManagerId { get; set; }

        [Display(Name = "Manager")]
        public string ManagerName { get; set; }

        [Display(Name = "Role")]
        public int RoleId { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }


        public List<EmployeePhoneViewModel>? Phones { get; set; }
    }
}
