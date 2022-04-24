using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCRouteSystem.Models
{
    [Display(Name = "Rota")]
    public class Route
	{
        [Key]
        public int Id { get; set; }
        [NotMapped]
        public List<string> Error { get; set; } = new List<string>();

        public Route()
        {
        }

        public void AddError(string error)
        {
            Error.Add(error);
        }

    }
}
