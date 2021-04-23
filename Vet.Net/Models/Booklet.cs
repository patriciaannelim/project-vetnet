using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Vet.Net.Models
{
    public class Booklet
    {

        [Key]
        public int BookletID { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Name of Pet")]
        public string PetName { get; set; }


        [Required(ErrorMessage = "Required")]
        public string Breed { get; set; }


        [Required(ErrorMessage = "Required")]
        public DateTime Birthday { get; set; } //updated


        [Required(ErrorMessage = "Required")]
        public Sex Sex { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Spayed or Neutered ")]
        public Neutered Neutered { get; set; }


        [Display(Name = "Food Sensitivities")]
        public string FoodSensitivities { get; set; }


        [Display(Name = "Additional Description")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Required")]
        [Display(Name = "Medications")]
        public string Meds { get; set; }
        //public Vaccine Vaccine { get; set; }

        [Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; }


        [Display(Name = "Date Modified")]
        public DateTime? DateModified { get; set; }

        public virtual Profile Profile { get; set; }
        public int? ProfileID { get; set; }
    }

    public enum Sex
    {
        Male = 1,
        Female = 2
    }

    public enum Neutered
    {
        Yes = 1,
        No = 2
    }
}
