using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PruebaGym2.Models
{
    public class MaquinasPorOrdenModel
    {
        public string TipoOrden { get; set; }
        public List<SelectListItem> OrdenList { get; set; }
        public List<Maquina> Maquinas { get; set; }
        public int VidaUtilRestante { get; set; }
        public int? LocalId { get; set; }
        public SelectList LocalList { get; set; }
    }
}
