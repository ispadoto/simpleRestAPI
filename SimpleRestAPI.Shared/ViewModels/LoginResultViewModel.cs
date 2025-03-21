using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRestAPI.Shared.ViewModels
{
    public class LoginResultViewModel
    {
        public string Token { get; set; }
        public int RoleId { get; set; }
    }
}
