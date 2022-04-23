using System.ComponentModel.DataAnnotations;

namespace ToolWorkshop.Data.Entities
{
    public class Catalog
    {
        [Display(Name = "SKU")]
        public int Id { get; set; }

        [Display(Name = "Herramienta")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        public Tool toolId { get; set; }

        [Display(Name = "Planograma")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        public Planogram planogramId { get; set; }

        [Display(Name = "Foto")]
        public Guid ToolImageId { get; set; }

        [Display(Name = "Foto")]
        public string ImageFullPath => ToolImageId == Guid.Empty
            ? $"https://localhost:7057/images/noimage.png"
            : $"https://shoppingzulu.blob.core.windows.net/users/{ToolImageId}";

        public virtual ICollection<Movement_Detail>? MovementDetails { get; set; }
    }
}
