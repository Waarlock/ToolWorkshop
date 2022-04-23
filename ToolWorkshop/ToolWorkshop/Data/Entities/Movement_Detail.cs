using System.ComponentModel.DataAnnotations;
using ToolWorkshop.Enums;

namespace ToolWorkshop.Data.Entities
{
    public class Movement_Detail
    {
        public int Id { get; set; }

        [Display(Name = "Movimiento")]
        public Temporal_Movement Temporal_MovementId { get; set; }

        [Display(Name = "Movimiento")]
        public Movement MovementId { get; set; }

        [Display(Name = "Catalogo")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        public Catalog CatalogId { get; set; }

        [Display(Name = "Observaciones")]
        [MaxLength(500, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        public string Remarks { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        MovementDetailStatus MovementDetailStatus { get; set; }

        [Display(Name = "Observaciones de Devolucion")]
        [MaxLength(500, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        public string? Retun_Remarks { get; set; }

    }
}
