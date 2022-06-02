using System.ComponentModel.DataAnnotations;
using ToolWorkshop.Data.Entities;
using ToolWorkshop.Enums;

namespace ToolWorkshop.Models
{
    public class ShowCartViewModel
    {
        [Display(Name = "Usuario")]
        public User User { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Comentarios")]
        public string Remarks { get; set; }

        public IEnumerable<Movement_Detail> Movement_Details { get; set; }
        public IEnumerable<IGrouping<Tool, Movement_Detail>> Movement_Details_Grouped { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Cantidad")]
        public float GeneralQuantity { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Cantidad")]
        public float ItemQuantity { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Codigo")]
        public string EAN { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        public MovementStatus Status { get; set; }

        public Tool tool { get; set; }

        public Catalog catalog { get; set; }

        [Display(Name = "Fecha de Inicio")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]

        public DateTime Start_DateTime { get; set; }

    }
}