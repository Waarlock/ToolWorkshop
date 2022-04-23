using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ToolWorkshop.Data.Entities
{
    public class State
    {
        public int Id { get; set; }

        [Display(Name = "Departamento")]
        [MaxLength(64, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }

        [JsonIgnore]
        public Country CountryId { get; set; }

        public ICollection<City>? Cities { get; set; }
    }
}
