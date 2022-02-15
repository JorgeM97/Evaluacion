using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion.Models.ViewModels
{
    /// <summary>
    /// Objeto que permite almacenar la información con las nuevas ventas
    /// </summary>
    public class NuevaVentaViewModel
    {
        public int IdInventario { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
    }
}
