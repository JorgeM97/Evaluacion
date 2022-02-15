using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion.Models.ViewModels
{
    /// <summary>
    /// Objeto para imprimir las ventas totales
    /// </summary>
    public class TotalesViewModel
    {
        public List<VentasViewModel> Ventas { get; set; }
        public decimal Total { get; set; }
    }
}
