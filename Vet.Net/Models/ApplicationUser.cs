using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vet.Net.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Address { get; set; }

        [Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; }


        [Display(Name = "Date Modified")]
        public DateTime? DateModified { get; set; }

        public UserTypes UserType { get; set; }
    }

    public enum UserTypes { 
        Admin = 1,
        PetOwner = 2
    }
}
