using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion.Models.Entities
{
    /// <summary>
    /// Tabla RegistroErroes
    /// </summary>
    public class RegistroErrores
    {
        public int Id { get; set; }
        public DateTime FechaError { get; set; }
        public string Mensaje { get; set; }
        public string Excepcion { get; set; }
        public int Codigo { get; set; }
        public string Operacion { get; set; }
    }
}
