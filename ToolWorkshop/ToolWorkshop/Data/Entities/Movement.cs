using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToolWorkshop.Enums;

namespace ToolWorkshop.Data.Entities
{
    public class Movement
    {
        public int Id { get; set; }

        [Display(Name = "Fecha de Inicio")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        public DateTime Start_DateTime { get; set; }

        [Display(Name = "Fecha de Finalizacion")]
        public DateTime End_DateTime { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        MovementStatus status { get; set; }

        public User User { get; set; }

        [Display(Name = "Detalles de Movimiento")]
        public virtual ICollection<Movement_Detail>? Details { get; set; }


    }
}
