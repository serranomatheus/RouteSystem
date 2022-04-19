using System.ComponentModel.DataAnnotations;

namespace MVCRouteSystem.Models
{
        [Display(Name = "Pessoa")]
        public class Person
        {
            public string Id { get; set; }
            [Display(Name = "Nome")]
            public string Name { get; set; }
            [Display(Name = "Time")]
            public Team Team { get; set; }
        }
    
}
