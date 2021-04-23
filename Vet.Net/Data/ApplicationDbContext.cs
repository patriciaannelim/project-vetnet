using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vet.Net.Models;

namespace Vet.Net.Data
{
    public class ApplicationDbContext :IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
             : base(options)
        {

        }

        public DbSet<ReservationForm> Reservations { get; set; }
        public DbSet<Medication> Medications { get; set; }
        
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<PetBooklet> PetBooklets { get; set; }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Booklet> Booklets { get; set; }
    }
}
