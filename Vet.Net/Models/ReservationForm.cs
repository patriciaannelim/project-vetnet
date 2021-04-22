using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;

namespace Vet.Net.Models
{
    public class ReservationForm
    {
        [Key]
        public int ReservationID { get; set; }

        [Display(Name = "Name of Pet")]
        public string PetName { get; set; }


        [Display(Name = "Medication Type")]
        public virtual Medication Medications { get; set; }
        public int? MedicationID { get; set; }


        [Required(ErrorMessage = "Required.")]
        [Display(Name = "Type of Pet")]
        public string Animal { get; set; }


        [Display(Name = "Date of Appointment")]
        public DateTime DatePicker { get; set; }


        [Display(Name = "Name of Pet Owner")]
        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }


        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid format")]
        [Required(ErrorMessage = "Required")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Required")]
        public string Phone { get; set; }


        [Required(ErrorMessage = "Required")]
        public string Address { get; set; }

        [Display(Name = "Payment Method")]
        public PaymentMode Type { get; set; }


        [Display(Name = "Customer Type")]
        public CustomerType CustomerType { get; set; }


        [Required(ErrorMessage = "Required")]
        [DataType(DataType.MultilineText)]
        public string Concerns { get; set; }


        [Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; }

        [Display(Name = "Date Modified")]
        public DateTime? DateModified { get; set; }


    }

    public enum PaymentMode
    {
        Cash = 1,
        Card = 2

    }


    public class Medication
    {
        public int MedicationID { get; set; }
        public string Name { get; set; }
    }

    public enum CustomerType
    {
        Reservation = 1,
        WalkIn = 2
    }
}
