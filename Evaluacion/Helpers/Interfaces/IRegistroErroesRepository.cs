using Evaluacion.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion.Helpers.Interfaces
{
    public interface IRegistroErroesRepository
    {
        Task<bool> RegistrarError(ErrorViewModel error); 
    }
}
