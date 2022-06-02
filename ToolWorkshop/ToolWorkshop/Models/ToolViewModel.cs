using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using ToolWorkshop.Data.Entities;
using ToolWorkshop.Utils;

namespace ToolWorkshop.Models
{
    public class ToolViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(500, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Description { get; set; }

        [Display(Name = "Codigo de Barras")]
        [MaxLength(18, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        public string EAN { get; set; }

        [Display(Name = "Categoría")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una categoría.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]

        public ICollection<Catalog> ToolCatalog { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Inventario")]
        public float Stock => ToolCatalog == null ? 0 : ToolCatalog.Count;

        public ICollection<ToolCategory> ToolCategories { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public ICollection<ToolImage> ToolImages { get; set; }

        [Display(Name = "Foto")]
        public Guid ImageId { get; set; }

        [Display(Name = "Foto")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://localhost:7057/images/noimage.png"
            : $"{Constants.ImageRepositoryRemote}/tools/{ImageId}";

        [Display(Name = "Image")]
        public IFormFile? ImageFile { get; set; }

        public Planogram planogram { get; set; }
        public int CategoryId { get; set; }
    }
}
