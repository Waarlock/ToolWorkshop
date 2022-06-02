using System.ComponentModel.DataAnnotations;
using ToolWorkshop.Data.Entities;

namespace ToolWorkshop.Models
{
    public class ShowCartViewModel
    {
        public User User { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Comentarios")]
        public string Remarks { get; set; }

        public ICollection<Temporal_Movement> Temporal_Movements { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Cantidad")]
        public float Quantity => Temporal_Movements == null ? 0 : 999;

        [DataType(DataType.MultilineText)]
        [Display(Name = "Codigo")]
        public string EAN { get; set; }

        public Tool tool { get; set; }

        public Catalog catalog { get; set; }
    }


}