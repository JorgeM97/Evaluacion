using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion.Models.ViewModels
{
    /// <summary>
    /// Objeto para imprimir la respuesta de la aplicación
    /// </summary>
    public class ServerResponseViewModel
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Mesagge { get; set; }
        public int Code { get; set; }
        public bool Succeedded { get; set; }
        public ServerResponseViewModel()
        {
            Id = 999;  
            Header = "Ocurrio un error al realizar la operación";
            Mesagge = "Favor de volver a intentar la operacion.";
            Code = 999;
            Succeedded = false;
        }
    }
}
