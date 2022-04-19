using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCRouteSystem.Models;

namespace MVCRouteSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MVCRouteSystem.Models.Route> Route { get; set; }
        public DbSet<MVCRouteSystem.Models.City> City { get; set; }
        public DbSet<MVCRouteSystem.Models.Person> Person { get; set; }
        public DbSet<MVCRouteSystem.Models.Team> Team { get; set; }
    }
}
