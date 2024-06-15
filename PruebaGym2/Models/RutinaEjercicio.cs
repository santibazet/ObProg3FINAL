using System.ComponentModel.DataAnnotations;

namespace PruebaGym2.Models
{
    public class RutinaEjercicio
    {
        [Display(Name = "Rutina")]
        public int IdRutina { get; set; }
        public Rutina? Rutina { get; set; }


        [Display(Name = "Ejercicio")]
        public int IdEjercicio { get; set; }
        public Ejercicio? Ejercicio { get; set; }


        [Display(Name = "Cantidad de sets")]
        [Range(1, 99, ErrorMessage = "El número debe ser mayor a 0 y tener un máximo de 2 cifras.")]
        public int Sets { get; set; }

        [Display(Name = "Cantidad de repeticiones")]
        [Range(1, 99, ErrorMessage = "El número debe ser mayor a 0 y tener un máximo de 2 cifras.")]
        public int Repeticiones { get; set; }

    }
}
