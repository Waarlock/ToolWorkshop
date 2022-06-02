using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToolWorkshop.Enums;
using ToolWorkshop.Utils;

namespace ToolWorkshop.Data.Entities
{
    public class Catalog
    {
        [Key]
        [Display(Name = "SKU")]
        public int SKU { get; set; }

        public int ToolId { get; set; }

        public int PlanogramId { get; set; }
        
        [Display(Name = "Herramienta")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        public Tool Tool { get; set; }

        [Display(Name = "Planograma")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        public Planogram Planogram { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        public CatalogStatus Status { get; set; }

        [Display(Name = "Foto")]
        public Guid ToolImageId { get; set; }

        [Display(Name = "Foto")]
        public string ImageFullPath => ToolImageId == Guid.Empty
            ? $"https://localhost:7057/images/noimage.png"
            : $"{Constants.ImageRepositoryRemote}/users/{ToolImageId}";

        public virtual ICollection<Movement_Detail>? MovementDetails { get; set; }


        [Display(Name = "Herramienta")]
        public string FullName => SKU != null && Tool != null && Tool.Name != null?  $"{SKU} - {Tool.Name}": "";

    }
}
