using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaGym2.Models
{
    public class Ejercicio
    {
        [Key]
        public int IdEjercicio { get; set; }

        [Required(ErrorMessage = "El nombre del ejercicio es obligatorio.")]
        [StringLength(60)]
        [Display(Name = "Nombre del ejercicio")]
        public string? NombreEjercicio { get; set; }

        [Required(ErrorMessage = "La descripción del ejercicio es obligatoria.")]
        [StringLength(100)]
        [Display(Name = "Descripción del ejercicio")]
        public string? DescripcionEjercicio { get; set; }

        

        [ForeignKey("TipoMaquina")]
        [Display(Name = "Máquina utilizada")]
        public int? TipoMaquinaId { get; set; }
        public TipoMaquina? TipoMaquina { get; set; }


    }
}
