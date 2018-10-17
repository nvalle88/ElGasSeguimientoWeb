using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCalidad.Models.ManageViewModels
{
    public class EspecificacionesGeneralesMaterial
    {
        public string DescripcionEspecificacion { get; set; }
        public string ValorEspecificacion { get; set; }

        public string NombreProveedor { get; set; }

        public string TipoMaterial { get; set; }

        public string UnidadMedida { get; set; }

        public string CodigoIngreso { get; set; }

        public string Identificador { get; set; }

        public decimal StockDisponible { get; set; }
    }
}
