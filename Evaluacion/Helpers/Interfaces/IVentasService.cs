using Evaluacion.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion.Helpers.Interfaces
{
    public interface IVentasService
    {
        Task<ServerResponseViewModel> RegistrarVenta(NuevaVentaViewModel nuevaVenta);
        Task<TotalesViewModel> ObtenerVentasSucursal(int? idSucursal);
        Task<TotalesViewModel> ObtenerVentasProductos(int? idProducto);
    }
}
