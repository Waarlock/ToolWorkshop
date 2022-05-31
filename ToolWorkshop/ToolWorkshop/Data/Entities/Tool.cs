using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToolWorkshop.Data.Entities
{
    public class Tool
    {
        //[Key]
        public int Id { get; set; }

        [Display(Name = "Codigo de Barras")]
        [MaxLength(18, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        public string EAN { get; set; }

        [Display(Name = "Nombre de Herramienta")]
        [MaxLength(64, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Descripcion de Herramienta")]
        [MaxLength(150, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Inventario")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public float Stock { get; set; }

        public ICollection<ToolCategory> ToolCategories { get; set; }

        [Display(Name = "Categorías")]
        public int CategoriesNumber => ToolCategories == null ? 0 : ToolCategories.Count;

        public ICollection<ToolImage> ToolImages { get; set; }

        [Display(Name = "Fotos")]
        public int ImagesNumber => ToolImages == null ? 0 : ToolImages.Count;

        //TODO: Pending to change to the correct path
        [Display(Name = "Foto")]
        public string ImageFullPath => ToolImages == null || ToolImages.Count == 0
            ? $"https://localhost:7005/images/noimage.png"
            : ToolImages.FirstOrDefault().ImageFullPath;
    }

   // public ICollection<SaleDetail> SaleDetails { get; set; }

    //public virtual ICollection<Category>? Categories { get; set; }


    // public virtual ICollection<Catalog>? Catalogs { get; set; }
}


