using System.ComponentModel.DataAnnotations;

namespace ToolWorkshop.Data.Entities
{
    public class Category_Tool
    {
        public int Id { get; set; }

        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        public Category CategoryId { get; set; }

        [Display(Name = "Herramienta")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        public Tool ToolId { get; set; }
    }
}
