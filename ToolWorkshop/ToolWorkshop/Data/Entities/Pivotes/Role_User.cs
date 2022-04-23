using System.ComponentModel.DataAnnotations;

namespace ToolWorkshop.Data.Entities
{
    public class Role_User
    {
        public int Id { get; set; }

        [Display(Name = "Rol")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        public Role RoleId { get; set; }

        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        public User UserId { get; set; }
    }
}
