using Evaluacion.Helpers.Interfaces;
using Evaluacion.Modelos.Context;
using Evaluacion.Models.Entities;
using Evaluacion.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion.Repositories
{
    public class RegistroErroresRepository : IRegistroErroesRepository
    {
        private PruebasContext _context;
        public RegistroErroresRepository(PruebasContext context) 
        {
            _context = context;
        }
        public async Task<bool> RegistrarError(ErrorViewModel error)
        {
            RegistroErrores generarError = new RegistroErrores();
            generarError.Mensaje = error.Mensaje.Count() > 499 ? error.Mensaje.Substring(0,499) : error.Mensaje;
            generarError.Excepcion = error.Mensaje.Count() > 249 ? error.Excepcion.Substring(0,249) : error.Excepcion;
            generarError.FechaError = DateTime.Now;
            generarError.Operacion = error.Operacion;
            generarError.Codigo = error.Codigo;
            _context.RegistroErrores.Add(generarError);
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}
