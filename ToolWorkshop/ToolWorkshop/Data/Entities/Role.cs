using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToolWorkshop.Data.Entities
{
    public class Role
    {
        public int Id { get; set; }

        [Display(Name = "Rol")]
        [MaxLength(30, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        public string Name { get; set; }

        [Display(Name = "Descripcion")]
        [MaxLength(150, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        public string Description { get; set; }

        public virtual ICollection<User>? Users { get; set; }

    }
}
