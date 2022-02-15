using Evaluacion.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion.Helpers.Interfaces
{
    public interface IVentasRepository
    {
        Task<TotalesViewModel> ObtenerVentasProductos(int? idProducto);
        Task<TotalesViewModel> ObtenerVentasSucursal(int? idSucursal);
        Task<bool> RegistrarVenta(NuevaVentaViewModel idProductoInventario);
    }
}
