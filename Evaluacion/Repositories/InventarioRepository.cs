using Evaluacion.Helpers;
using Evaluacion.Helpers.Interfaces;
using Evaluacion.Modelos.Context;
using Evaluacion.Models.Entities;
using Evaluacion.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Evaluacion.Repositories
{
    public class InventarioRepository : IInventarioRepository
    {
        private PruebasContext _context;
        public InventarioRepository(PruebasContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Actualizar la información de un producto que ya exíste en el inventario
        /// </summary>
        /// <param name="productoInventario">Objeto con la información del producto</param>
        /// <returns>bool</returns>
        public async Task<bool> ActualizarProductoInventario(ProductoInventarioViewModel productoInventario)
        {
            var actualizarProducto = _context.Inventario.Where(x => x.Id == productoInventario.Id).FirstOrDefault();
            actualizarProducto.Precio = productoInventario.Precio;
            actualizarProducto.Cantidad = productoInventario.Cantidad;
            return await _context.SaveChangesAsync() >= 0;
        }

        /// <summary>
        /// Agregar la información de un nuevo producto a la base de datos.
        /// </summary>
        /// <param name="nuevoProducto">Objeto con la información del nuevo producto</param>
        /// <returns>bool</returns>
        public async Task<bool> AgregarNuevoProductoInventario(ProductoInventarioViewModel nuevoProducto)
        {
            Inventario inventario = new Inventario();
            inventario.IdProducto = nuevoProducto.IdProducto;
            inventario.IdSucursal = nuevoProducto.IdSucursal;
            inventario.Precio = nuevoProducto.Precio;
            inventario.Cantidad = nuevoProducto.Cantidad;
            _context.Inventario.Add(inventario);
            return await _context.SaveChangesAsync() >= 0;
        }

        /// <summary>
        /// Agregar la información de una nueva sucursal a la base de datos.
        /// </summary>
        /// <param name="sucursal">Objeto con la información de la sucursal</param>
        /// <returns>bool</returns>
        public async Task<bool> AgregarSucursal(SucursalesViewModel sucursal)
        {
            Sucursales nuevaSucursal = new Sucursales();
            nuevaSucursal.Sucursal = sucursal.Sucursal;
            _context.Add(sucursal);
            return await _context.SaveChangesAsync() >=  0;
        }

        /// <summary>
        /// Eliminar la información de un producto en inventario de la base de datos.
        /// </summary>
        /// <param name="idProductoInventario">Identificador del producto en inventario</param>
        /// <returns>bool</returns>
        public async Task<bool> EliminarProductoInventario(int idProductoInventario)
        {
            var productoInventario = _context.Inventario.Where(x => x.Id == idProductoInventario).FirstOrDefault();
            _context.Inventario.Remove(productoInventario);
            return await _context.SaveChangesAsync() >= 0;
        }

        /// <summary>
        /// Obtener la información del inventario por sucursal.
        /// </summary>
        /// <param name="idSucursal">Identificador de la sucursal</param>
        /// <returns>Lista de los objetos con la información de los inventarios</returns>
        public async Task<List<InventarioViewModel>> ObtenerInventario(int? idSucursal)
        {
            List<InventarioViewModel> respuesta = new List<InventarioViewModel>();

            if (idSucursal != null)
            {
                respuesta = _context.Inventario.Where(x => x.IdSucursal == idSucursal).
                    Select(x => new InventarioViewModel { 
                    Id = x.Id,
                    Nombre = x.IdProductoNavigation.Nombre,
                    Cantidad = x.Cantidad,
                    CodigoBarras = x.IdProductoNavigation.CodigoBarras,
                    Precio = x.Precio,
                    Sucursal = x.IdSucursalNavigation.Sucursal
                    }).OrderByDescending(x => x.Sucursal).ThenBy(x => x.Nombre).ToList();
            }
            else {
                respuesta = _context.Inventario.Select(
                    x => new InventarioViewModel
                    {
                        Id = x.Id,
                        Nombre = x.IdProductoNavigation.Nombre,
                        Cantidad = x.Cantidad,
                        CodigoBarras = x.IdProductoNavigation.CodigoBarras,
                        Precio = x.Precio,
                        Sucursal = x.IdSucursalNavigation.Sucursal
                    }).OrderByDescending(x => x.Sucursal).ThenBy(x => x.Nombre).ToList();
            }
            
            return respuesta;
        }

        /// <summary>
        /// Obtener la información de los inventarios por producto y/o sucursal.
        /// </summary>
        /// <param name="idProducto">Identificador del producto</param>
        /// <param name="idSucursal">Identificador de la sucursal</param>
        /// <returns>Lista de los objetos con la información de los inventarios</returns>
        public async Task<List<InventarioViewModel>> ObtenerProductosSucursal(int idProducto, int? idSucursal)
        {
            List<InventarioViewModel> respuesta = new List<InventarioViewModel>();

            if (idSucursal == null) {
                var productos = _context.Inventario.Where(x => x.IdProducto == idProducto && x.IdSucursal == idSucursal)
                    .Select(x => new InventarioViewModel
                    {
                        Nombre = x.IdProductoNavigation.Nombre,
                        Cantidad = x.Cantidad,
                        Sucursal = x.IdSucursalNavigation.Sucursal,
                        CodigoBarras = x.IdProductoNavigation.CodigoBarras,
                        Precio = x.Precio,
                    }).ToList().OrderByDescending(x => x.Precio);
            }
            else {
                var productos = _context.Inventario.Where(x => x.IdProducto == idProducto)
                    .Select(x => new InventarioViewModel
                    {
                    Nombre = x.IdProductoNavigation.Nombre,
                    Cantidad = x.Cantidad,
                    Sucursal = x.IdSucursalNavigation.Sucursal,
                    CodigoBarras = x.IdProductoNavigation.CodigoBarras,
                    Precio = x.Precio,
                    }).ToList().OrderByDescending(x => x.Precio);
                 }

            return respuesta;
        }

        /// <summary>
        /// Obtener el listado de las sucursales registradas.
        /// </summary>
        /// <returns>Lista de objetos con la información de las sucursales</returns>
        public async Task<List<SucursalesViewModel>> ObtenerSucursales()
        {
            return  _context.Sucursales.Select(x => new SucursalesViewModel {
                Id = x.Id,
                Sucursal = x.Sucursal
            }).ToList();
        }

        /// <summary>
        /// Validar si el nombre de la sucursal ya exíste en base de datos.
        /// </summary>
        /// <param name="nombreSucursal">Nombre de la sucursal</param>
        /// <returns>bool</returns>
        public async Task<bool> ValidarNombreSucursal(string nombreSucursal)
        {
            return _context.Sucursales.Any(x => x.Sucursal == nombreSucursal);
        }

        /// <summary>
        /// Validar si el producto del inventario ya exíste.
        /// </summary>
        /// <param name="idInventario">Identificador del producto en inventario</param>
        /// <returns>bool</returns>
        public async Task<bool> ValidarProductoInventario(int idInventario)
        {
            return _context.Inventario.Any(x => x.Id == idInventario);
        }

        /// <summary>
        /// Validar si la sucursal indicada existe
        /// </summary>
        /// <param name="idSucursal">Identificador de la sucursal</param>
        /// <returns>bool</returns>
        public async Task< bool> ValidarSucursal(int idSucursal)
        {
            return _context.Sucursales.Any(x => x.Id == idSucursal);
        }
    }
}
