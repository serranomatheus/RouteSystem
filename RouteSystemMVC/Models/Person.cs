using System.ComponentModel.DataAnnotations;

namespace RouteSystemMVC.Models
{
    [Display(Name = "Pessoa")]
    public class Person
    {
        public string Id { get; set; }
        [Display(Name ="Nome")]
        public string Name { get; set; }
        [Display(Name = "Time")]
        public Team Team { get; set; }
    }
}