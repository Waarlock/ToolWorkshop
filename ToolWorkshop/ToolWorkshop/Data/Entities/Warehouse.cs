using System.ComponentModel.DataAnnotations;

namespace ToolWorkshop.Data.Entities
{
    public class Warehouse
    {
        public int Id { get; set; }

        [Display(Name = "Almacen")]
        [MaxLength(32, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        public string Name { get; set; }

        [Display(Name = "Descripcion")]
        [MaxLength(64, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        public string Description { get; set; }

        [Display(Name = "Ciudad")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        public City City { get; set; }

        public ICollection<Planogram>? Planograms { get; set; }

    }
}
