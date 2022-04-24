using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [NotMapped]
        public List<string> Error { get; set; } = new List<string>();

        public void AddError(string error)
        {
            Error.Add(error);
        }
    }
    
}
