using System.ComponentModel.DataAnnotations;

namespace ToolWorkshop.Models
{
    public class AddToolImageViewModel
    {
        public int ToolId { get; set; }

        [Display(Name = "Foto")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public IFormFile ImageFile { get; set; }

    }
}
