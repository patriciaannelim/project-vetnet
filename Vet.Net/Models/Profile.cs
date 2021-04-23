using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vet.Net.Models
{
    public class Profile
    {

        [Key]
        public int ProfileID { get; set; }


        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }


        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid format")]
        [Required(ErrorMessage = "Required")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Required")]
        public string Phone { get; set; }


        [Required(ErrorMessage = "Required")]
        public string Address { get; set; }

       
        [Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; }


        [Display(Name = "Date Modified")]
        public DateTime? DateModified { get; set; }

        public IList<Booklet> Booklets { get; set; }
        //[Required(ErrorMessage = "Required.")]
        //public virtual Booklet Booklet { get; set; }

        //public int? BookletID { get; set; }

    }
}
