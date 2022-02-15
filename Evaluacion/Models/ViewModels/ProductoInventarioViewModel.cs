using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion.Models.ViewModels
{
    /// <summary>
    /// Objeto para almacenar la información del producto y la sucursal a la que pertenece
    /// </summary>
    public class ProductoInventarioViewModel
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public int IdSucursal { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public ProductoInventarioViewModel() {
            IdProducto = 0;
            IdSucursal = 0;
            Precio = 0;
            Cantidad = 0;
        }
    }
}
