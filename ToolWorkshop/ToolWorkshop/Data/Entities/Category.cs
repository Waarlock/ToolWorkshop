using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToolWorkshop.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Display(Name = "Categoría")]
        [MaxLength(32, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        public string Name { get; set; }

        public ICollection<ToolCategory> ToolCategories { get; set; }
        //public virtual ICollection<Tool>? Tools { get; set; }
    }
}
