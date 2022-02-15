using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion.Models.ViewModels
{
    /// <summary>
    /// Objeto con la información recopilada de las excepciones presentadas.
    /// </summary>
    public class ErrorViewModel
    {
        public DateTime FechaError { get; set; }
        public string Mensaje { get; set; }
        public string Excepcion { get; set; }
        public int Codigo { get; set; }
        public string Operacion { get; set; }
    }
}
