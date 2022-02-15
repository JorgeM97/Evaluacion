using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion.Models.Entities
{
    /// <summary>
    /// Tabla RegistroVentas
    /// </summary>
    public class RegistroVentas
    {
        public int Id { get; set; }
        public DateTime Fecha {get; set;}
        public decimal PrecioVenta { get; set; }
        public int IdProducto { get; set; }
        public int IdSucursal { get; set; }
        public int Cantidad { get; set; }
        public virtual Productos IdProductoNavigation { get; set; }
        public virtual Sucursales IdSucursalNavigation { get; set; }
    }
}
