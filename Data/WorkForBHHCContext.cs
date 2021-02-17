using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkForBHHC.Data.Entities;

namespace WorkForBHHC.Data
{
    public class WorkForBHHCContext : IdentityDbContext<StoreUser>
    {
        public WorkForBHHCContext(DbContextOptions<WorkForBHHCContext> options): base(options)
        {
        }
        public DbSet<Reason> Reasons { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
