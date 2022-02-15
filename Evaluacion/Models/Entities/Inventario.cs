using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion.Models.Entities
{
    /// <summary>
    /// Tabla Inventario
    /// </summary>
    public class Inventario
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public int IdProducto { get; set; }
        public int IdSucursal { get; set; }
        public virtual Productos IdProductoNavigation { get; set; }
        public virtual Sucursales IdSucursalNavigation { get; set; }
    }
}
