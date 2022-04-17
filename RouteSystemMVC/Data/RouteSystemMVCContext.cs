using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RouteSystemMVC.Models;

namespace RouteSystemMVC.Data
{
    public class RouteSystemMVCContext : DbContext
    {
        public RouteSystemMVCContext (DbContextOptions<RouteSystemMVCContext> options)
            : base(options)
        {
        }

        public DbSet<RouteSystemMVC.Models.Team> Team { get; set; }

        public DbSet<RouteSystemMVC.Models.Person> Person { get; set; }

        public DbSet<RouteSystemMVC.Models.City> City { get; set; }

        public DbSet<RouteSystemMVC.Models.Route> Route { get; set; }
    }
}
