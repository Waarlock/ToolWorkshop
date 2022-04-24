using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using ToolWorkshop.Enums;

namespace ToolWorkshop.Data.Entities
{
    public class User : IdentityUser
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

        [Display(Name = "Password")]
        [MaxLength(512, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        public string Password { get; set; }

        [Display(Name = "Correo")]
        [MaxLength(64, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        public string Email { get; set; }

        [Display(Name = "Tipo de Documento")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        public DocumentType DocumentType { get; set; }

        [Display(Name = "Numero de Documento")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        public String Document { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        public UserStatus Status { get; set; }

        [Display(Name = "Tipo de usuario")]
        public UserType UserType { get; set; }

        //TODO: Organizar
        [Display(Name = "Foto")]
        public Guid ImageId { get; set; }

        //TODO: Pending to put the correct paths
        [Display(Name = "Foto")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://localhost:7057/images/noimage.png"
            : $"https://shoppingzulu.blob.core.windows.net/users/{ImageId}";

        public ICollection<Role_User>? Role_Users;
        public virtual ICollection<Role>? Roles { get; set; }

        [Display(Name = "Usuario")]
        public string FullName => $"{Name} {LastName}";

        [Display(Name = "Usuario")]
        public string FullNameWithDocument => $"{Name} {LastName} - {Document}";
    }
}
