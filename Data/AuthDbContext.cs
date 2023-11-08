using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Demo.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
                
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var readerId= "b902ba31-84cc-4f0a-8532-d40b9d891fc8";
            var writerId= "32f29cef-bfed-4e5d-802b-e2c1fd898bcb";

            var roles= new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id= readerId,
                    ConcurrencyStamp= readerId,
                    Name= "Reader",
                    NormalizedName= "Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id= writerId,
                    ConcurrencyStamp= writerId,
                    Name= "Writer",
                    NormalizedName= "Writer".ToUpper()
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}