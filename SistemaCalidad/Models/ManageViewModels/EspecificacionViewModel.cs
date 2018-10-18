using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElGasSeguimientoWeb.Models
{
    public class EspecificacionViewModel
    {
        public int EspecificacionId { get; set; }
        public int MaterialId { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }
        public string Clase { get; set; }
        public string ValorEsperadoProducto { get; set; }
        public decimal? RangoMinimoProducto { get; set; }
        public decimal? RangoMaximoProducto { get; set; }
        public string ValorMaterial { get; set; }
        public bool? Analisis { get; set; }
    }
}
