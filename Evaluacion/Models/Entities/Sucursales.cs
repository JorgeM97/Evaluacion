using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion.Models.Entities
{
    /// <summary>
    /// Tabla sucursales
    /// </summary>
    public class Sucursales
    {
        public Sucursales()
        {
            Inventario = new HashSet<Inventario>();
            RegistroVentas = new HashSet<RegistroVentas>();
        }
        public int Id { get; set; }
        public string Sucursal { get; set; }
        public ICollection<Inventario> Inventario { get; set; }
        public ICollection<RegistroVentas> RegistroVentas { get; set; }
    }
}
