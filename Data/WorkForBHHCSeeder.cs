using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkForBHHC.Data.Entities;

namespace WorkForBHHC.Data
{
    public class WorkForBHHCSeeder
    {
        private readonly WorkForBHHCContext _ctx;
        private readonly IWebHostEnvironment _hosting;
        private readonly UserManager<StoreUser> _userManager;
        public WorkForBHHCSeeder(WorkForBHHCContext ctx, IWebHostEnvironment hosting, UserManager<StoreUser> userManager)
        {
            _ctx = ctx;
            _hosting = hosting;
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {
            _ctx.Database.EnsureCreated();
            StoreUser user = await _userManager.FindByNameAsync("aryar@test.com");
            if (user == null)
            {
                user = new StoreUser()
                {
                    FirstName = "Rajiv",
                    LastName = "Arya",
                    Email = "aryar@test.com",
                    UserName = "aryar@test.com"
                };
                var result = await _userManager.CreateAsync(user, "P@ssw0rd!");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create new user in Seeder");
                }
            }
            var reason = _ctx.Reasons.Where(r => r.Id == 1).FirstOrDefault();
            if (reason != null)
            {
                reason.Description = "Created from Seeder";
                reason.CreatedOn = DateTime.Now;
                reason.User = user;
            }
            _ctx.SaveChanges();
        }
    }
}
