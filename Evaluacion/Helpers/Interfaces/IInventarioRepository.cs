using Evaluacion.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion.Helpers.Interfaces
{
    public interface IInventarioRepository
    {
        Task<List<InventarioViewModel>> ObtenerInventario(int? idSucursal);
        Task<List<InventarioViewModel>> ObtenerProductosSucursal(int idProducto, int? Sucursal);
        Task<bool> ValidarSucursal(int idSucursal);
        Task<bool> ValidarProductoInventario(int idInventario);     
        Task<bool> ActualizarProductoInventario(ProductoInventarioViewModel productoInventario);
        Task<bool> AgregarNuevoProductoInventario(ProductoInventarioViewModel nuevoProducto);
        Task<bool> EliminarProductoInventario(int idProductoInventario);
        Task<List<SucursalesViewModel>> ObtenerSucursales();
        Task<bool> ValidarNombreSucursal(string nombreSucursal);
        Task<bool> AgregarSucursal(SucursalesViewModel sucursal);
    }
}
