using System.ComponentModel.DataAnnotations;

namespace PruebaGym2.Models
{
    public class TipoMaquina
    {

        [Key]
        public int IdTipoMaquina { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nombre del tipo")]
        public string NombreTipo { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Descripción del tipo")]
        public string? Descripcion { get; set; }

        public ICollection<Maquina>? Maquinas { get; set; }

    }
}
