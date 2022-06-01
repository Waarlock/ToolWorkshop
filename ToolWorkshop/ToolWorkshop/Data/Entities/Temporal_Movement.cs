using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ToolWorkshop.Enums;

namespace ToolWorkshop.Data.Entities
{
    public class Temporal_Movement
    {
        public int Id { get; set; }

        public User User { get; set; }

        public Tool Tool{ get; set; }


        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public float Quantity { get; set; }

        [Display(Name = "Fecha de Inicio")]
      //  [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        public DateTime Start_DateTime { get; set; }

        [Display(Name = "Fecha de Finalizacion")]
        public DateTime End_DateTime { get; set; }

        [Display(Name = "Estado")]
     //   [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        MovementStatus status { get; set; }
           

        [Display(Name = "Detalles de Movimiento")]
        public virtual ICollection<Movement_Detail>? Details { get; set; }


    }
}
