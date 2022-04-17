using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RouteSystemMVC.Models
{
    [Display(Name = "Time")]
    public class Team
    {
        public string Id { get; set; }
        [Display(Name = "Nome")]
        [Required(ErrorMessage ="campo obrigatorio")]
        public string Name { get; set; }
        [Display(Name = "Membros")]
        public string Members { get; set; }
        [Display(Name = "Cidade")]
        public string City { get; set; }
        public List<string> Error { get; set; } = new List<string>();

        public void AddError(string error)
        {
            Error.Add(error);
        }
    }
}
