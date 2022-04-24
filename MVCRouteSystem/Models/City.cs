using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [NotMapped]
        public List<string> Error { get; set; } = new List<string>();

        public void AddError(string error)
        {
            Error.Add(error);
        }
    }
}
