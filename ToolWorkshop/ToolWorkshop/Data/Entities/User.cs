using System.ComponentModel.DataAnnotations;

namespace ToolWorkshop.Data.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Display(Name = "Nombres")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        public string Name { get; set; }

        [Display(Name = "Apellidos")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        public string LastName { get; set; }

        [Display(Name = "Login")]
        [MaxLength(16, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        public string UserName { get; set; }

        public virtual ICollection<Role>? Roles { get; set; }



        [Display(Name = "Usuario")]
        public string FullName => $"{Name} {LastName}";

        [Display(Name = "Usuario")]
        public string FullNameWithDocument => $"{Name} {LastName} - {Document}";
    }
}
