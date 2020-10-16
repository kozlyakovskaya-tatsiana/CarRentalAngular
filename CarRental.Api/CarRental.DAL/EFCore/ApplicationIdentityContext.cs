using System;
using System.Collections.Generic;
using System.Text;
using CarRental.DAL.Entities;
using CarRental.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Identity.EFCore
{
    public class ApplicationIdentityContext : IdentityDbContext<User, IdentityRole, string>
    {
        public ApplicationIdentityContext(DbContextOptions<ApplicationIdentityContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
