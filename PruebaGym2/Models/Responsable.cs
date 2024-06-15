﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaGym2.Models
{
    public class Responsable
    {
        [Key]
        public int idResponsable { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nombre y apellido del responsable")]
        public string? Nombre { get; set; }

        [Required]
        [Display(Name = "Teléfono del responsable")]
        [RegularExpression(@"^(\d{8,9})$", ErrorMessage = "El número de teléfono debe tener entre 8 y 9 dígitos.")]
        public int Telefono { get; set; }

    }
}
