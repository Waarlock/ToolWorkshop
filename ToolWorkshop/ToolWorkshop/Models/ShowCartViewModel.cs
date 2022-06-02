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
        public float Quantity => Temporal_Movements == null ? 0 : Temporal_Movements.Sum(ts => ts.Quantity);

        [DataType(DataType.MultilineText)]
        [Display(Name = "Codigo")]
        public string EAN { get; set; }
       
    }
}
