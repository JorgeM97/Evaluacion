using Evaluacion.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion.Helpers.Interfaces
{
    public interface IInventarioServices
    {
        Task<List<InventarioViewModel>> ObtenerInventario(int? idSucursal);
        Task<List<InventarioViewModel>> ObtenerProductosSucursal(int? idProducto, int? idSucursal);
        Task<ServerResponseViewModel> AgregarProductoInventario(ProductoInventarioViewModel productoInventario);
        Task<ServerResponseViewModel> ActualziarProductoInventario(ProductoInventarioViewModel productoInventario);
        Task<ServerResponseViewModel> EliminarProductoInventario(int? idProducto);
        Task<List<SucursalesViewModel>> ObtenerSucursales();
        Task<ServerResponseViewModel> AgregarSucursal(SucursalesViewModel sucursal);
    }
}
