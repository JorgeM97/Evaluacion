using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion.Models.ViewModels
{
    /// <summary>
    /// Objeto para imprimir las ventas por producto y sucursal
    /// </summary>
    public class VentasViewModel
    {
        public int Id { get; set; }
        public string FechaVenta { get; set; }
        public decimal PrecioVenta { get; set; }
        public string Sucursal { get; set; }
        public string Producto { get; set; }
    }
}
