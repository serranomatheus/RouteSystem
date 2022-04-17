using System.Collections.Generic;

namespace RouteSystemMVC.Models
{
    public class Team
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Members { get; set; }
        public City City { get; set; }
    }
}
