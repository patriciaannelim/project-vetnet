using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Vet.Net.Models
{
    public class UserProfile //not yet migrated to database (updated one)
    {
        [Key]

       
        public int UserID { get; set; }


        [Required(ErrorMessage = "Required.")]

        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }


        [Required(ErrorMessage ="Required.")]
        public virtual PetBooklet PetBooklet { get; set; }
        public int? PetID { get; set; }
    }

    public class PetBooklet
    {
        [Key]
        public int PetID { get; set; }

        [Display(Name = "Name of Pet")]
        public string PetName { get; set; }
        public string Breed { get; set; }
        public DateTime Birthday { get; set; }
        public Gender Gender { get; set; }


        [Display(Name = "Spayed or Neutered ")]
        public Spayed Spayed { get; set; }


        [Display(Name = "Food Sensitivities")]
        public string FoodSensitivities { get; set; }


        [Display(Name = "Additional Description")]
        public string Description { get; set; }

        public string Meds { get; set; }
        //public Vaccine Vaccine { get; set; }

    }

    public enum Gender
    {
        Male = 1,
        Female = 2
    }

    public enum Spayed
    {
        Yes = 1,
        No = 2
    }

    //public enum Vaccine
    //{
    //    Yes = 1,
    //    No = 2
    //}

}
