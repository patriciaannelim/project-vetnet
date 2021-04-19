using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vet.Net.Models;

namespace Vet.Net.Data
{
    public class ReserveDbContext : IdentityDbContext<ReserveUser>
    {
        public ReserveDbContext(DbContextOptions<ReserveDbContext> options)
            : base(options)
        {

        }
        public DbSet<ReserveUser> Names { get; set; }
    }
   
}
