using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Entities
{
    public class AppIdentityUser : IdentityUser
    {
        [MaxLength(300)]
        public string Avatar { get; set; }
        public string EmaillUser { get; set; }



    }
}
