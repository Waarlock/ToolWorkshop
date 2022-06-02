using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToolWorkshop.Enums;

namespace ToolWorkshop.Data.Entities
{
    public class Movement_Detail
    {
        public int Id { get; set; }

        [Display(Name = "Movimiento")]
        public Temporal_Movement? Temporal_Movement { get; set; }

        [Display(Name = "Movimiento")]
        public Movement? Movement { get; set; }

        [Display(Name = "Catalogo")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        public Catalog Catalog { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Observaciones")]
        [MaxLength(500, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        public string? Remarks { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        MovementDetailStatus MovementDetailStatus { get; set; }

        [Display(Name = "Observaciones de Devolucion")]
        [MaxLength(500, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        public string? Return_Remarks { get; set; }

    }
}
