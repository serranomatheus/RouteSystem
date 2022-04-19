using System.ComponentModel.DataAnnotations;

namespace MVCRouteSystem.Models
{
    [Display(Name = "Cidade")]
    public class City
	{
        public string Id { get; set; }
        [Display(Name = "Nome")]
        public string Name { get; set; }
        [Display(Name = "Estado")]
        public string State { get; set; }
    }
}
