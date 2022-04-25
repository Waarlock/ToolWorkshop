using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ToolWorkshop.Data.Entities;
using ToolWorkshop.Utils;

namespace ToolWorkshop.Models
{
    public class CatalogCreateModel
    {
        public int id { get; set; }

        public int ToolId { get; set; }

        public int PlanogramId { get; set; }

        [Display(Name = "SKU")]
        public int SKU { get; set; }

        [Display(Name = "Herramienta")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        public Tool Tool { get; set; }


        
        [Display(Name = "Foto")]
        public Guid ToolImageId { get; set; }

        [Display(Name = "Foto")]
        public string ImageFullPath => ToolImageId == Guid.Empty
            ? $"https://localhost:7057/images/noimage.png"
            : $"https://{Constants.ImageRepositoryRemote}/users/{ToolImageId}";

        public IEnumerable<SelectListItem>? CatalogList { get; set; }

        Catalog catalog;

    }
}
