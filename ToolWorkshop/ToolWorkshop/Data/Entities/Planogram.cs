﻿using System.ComponentModel.DataAnnotations;

namespace ToolWorkshop.Data.Entities
{
    public class Planogram
    {
        public int Id { get; set; }

        [Display(Name = "Tipo de Planograma")]
        [MaxLength(32, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        public string Type { get; set; }

        [Display(Name = "Nombre de Planograma")]
        [MaxLength(32, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        public string Name { get; set; }

        [Display(Name = "Almacen")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        public Warehouse WarehouseId { get; set; }
    }
}
