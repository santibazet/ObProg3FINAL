using PruebaGym2.Datos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaGym2.Models
{
    public class Local
    {
        [Key]
        public int IdLocal { get; set; }

        [Required(ErrorMessage = "El nombre del local es obligatorio.")]
        [StringLength(50)]
        [Display(Name = "Nombre del local")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "La ciudad del local es obligatoria.")]
        [StringLength(80)]
        [Display(Name = "Ciudad del local")]
        public string? Ciudad { get; set; }

        [Required(ErrorMessage = "La dirección del local es obligatoria.")]
        [StringLength(100)]
        [Display(Name = "Dirección del local")]
        public string? Direccion { get; set; }

        [Required(ErrorMessage = "El teléfono del local es obligatorio.")]
        [Display(Name = "Teléfono del local")]
        [RegularExpression(@"^(\d{8,9})$", ErrorMessage = "El número de teléfono debe tener entre 8 y 9 dígitos.")]
        public int Telefono { get; set; }

        public List<Maquina>? MaquinasDeLocal { get; set; }

        [Required(ErrorMessage = "El nombre del responsable es obligatorio.")]
        [Display(Name = "Nombre del responsable")]
        [ForeignKey("Responsable")]
        public int IdResponsable { get; set; }
        public Responsable? Responsable { get; set; }

        public ICollection<Socio>? SociosAfiliados { get; set; }



    }
}
