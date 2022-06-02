using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToolWorkshop.Data.Entities
{
    public class Planogram
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Tipo de Planograma")]
        [MaxLength(32, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        public string Type { get; set; }

        [Display(Name = "Nombre de Planograma")]
        [MaxLength(32, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        public string Name { get; set; }

        [Display(Name = "Almacen")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        public Warehouse Warehouse { get; set; }

        public virtual ICollection<Catalog>? Catalogs { get; set; }

        [Display(Name = "Herramienta")]
        public string FullName => $"{Warehouse.Name} - {Name}";
    }
}
