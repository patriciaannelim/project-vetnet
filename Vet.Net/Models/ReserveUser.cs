using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Vet.Net.Models;

namespace Vet.Net.Models
{
    public class ReserveUser : IdentityUser
    {
        public int petid { get; set; }
        public string petname { get; set; }
        public DateTime datepicker { get; set; }
        public paymentmode type { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public int phone { get; set; }
        public string city { get; set; }
        public string textarea { get; set; }
        
        public enum paymentmode
        {
            Cash = 1,
            Card = 2

        }
    }
}
