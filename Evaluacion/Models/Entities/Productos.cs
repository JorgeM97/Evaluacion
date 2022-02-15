using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion.Models.Entities
{
    /// <summary>
    /// Tabla Productos
    /// </summary>
    public class Productos
    {
        public Productos()
        {
            Inventario = new HashSet<Inventario>();
            RegistroVentas = new HashSet<RegistroVentas>();
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string CodigoBarras { get; set; }
        public ICollection<Inventario> Inventario { get; set; }
        public ICollection<RegistroVentas> RegistroVentas { get; set; }
    }
}
