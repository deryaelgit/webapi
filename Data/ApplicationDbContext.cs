using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }


        public DbSet<Stok> Stoklar { get; set; }
        public DbSet<Yorum> Yorumlar { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Portfolio>(x=>x.HasKey(p=> new{p.AppUserId,p.StokId}));


            builder.Entity<Portfolio>()
                .HasOne(u=>u.AppUser)
                .WithMany(u=>u.Portfolios)
                .HasForeignKey(p=>p.AppUserId);

            
            builder.Entity<Portfolio>()
                .HasOne(u=>u.Stok)
                .WithMany(u=>u.Portfolios)
                .HasForeignKey(p=>p.StokId);

            List<IdentityRole> roles= new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name="Admin",
                    NormalizedName="ADMIN"

                },
                 new IdentityRole
                {
                    Name="User",
                    NormalizedName="USER"

                },
            };
            builder.Entity<IdentityRole>().HasData(roles);

        }
        
    }
}