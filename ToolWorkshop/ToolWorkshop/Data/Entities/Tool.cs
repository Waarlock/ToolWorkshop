using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToolWorkshop.Data.Entities
{
    public class Tool
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Codigo de Barras")]
        [MaxLength(18, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        public string EAN { get; set; }

        [Display(Name = "Nombre de Herramienta")]
        [MaxLength(64, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        public string Name { get; set; }

        [Display(Name = "Descripcion de Herramienta")]
        [MaxLength(150, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        public string Description { get; set; }

        public virtual ICollection<Category>? Categories { get; set; }

        public virtual ICollection<Catalog>? Catalogs { get; set; }
    }
}
