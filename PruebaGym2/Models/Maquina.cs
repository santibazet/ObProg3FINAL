using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaGym2.Models
{
    public class Maquina
    {
        [Key]
        public int IdMaquina { get; set; }

        [Required(ErrorMessage = "La fecha de compra es obligatoria.")]
        [Display(Name = "Fecha de compra")]
        [DataType(DataType.Date)]
        public DateTime FechaCompra { get; set; }
        [Column(TypeName = "date")]

        [Required(ErrorMessage = "El precio de compra es obligatorio.")]
        [Display(Name = "Precio de compra ($U)")]
        [Range(1, 999999, ErrorMessage = "El precio debe ser mayor a 0 y tener un máximo de 6 cifras.")]
        public int PrecioCompra { get; set; }

        [Required(ErrorMessage = "La vida útil es obligatoria.")]
        [Display(Name = "Vida útil")]
        [Range(1, 99, ErrorMessage = "El número debe ser mayor a 0 y tener un máximo de 2 cifras.")]
        public int VidaUtil { get; set; }

        [Required(ErrorMessage = "La disponibilidad es obligatoria.")]
        [Display(Name = "Disponibilidad")]
        public bool Disponible { get; set; }

        [Display(Name = "Local al que pertenece")]
        [ForeignKey("Local")]
        public int IdLocal { get; set; }
        public Local? Local { get; set; }

        [Display(Name = "Tipo de máquina")]
        [ForeignKey("TipoMaquina")]
        public int IdTipoMaquina { get; set; }
        public TipoMaquina? TipoMaquina { get; set; }

        [Required]
        [Display(Name = "Cantidad")]
        public int Cantidad { get; set; }
    }
}
