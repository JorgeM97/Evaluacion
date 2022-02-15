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
    public class VentasRepository : IVentasRepository
    {
        private static PruebasContext _context;
        public VentasRepository(PruebasContext context) 
        {
            _context = context;
        }
        /// <summary>
        /// Registrar la información de una nueva venta
        /// </summary>
        /// <param name="nuevaVenta">Objeto con la información de la venta</param>
        /// <returns>bool</returns>
        public async Task<bool> RegistrarVenta(NuevaVentaViewModel nuevaVenta)
        {
            RegistroVentas registro = new RegistroVentas();

            var producto = _context.Inventario.Where(x => x.Id == nuevaVenta.IdInventario).FirstOrDefault();

            registro.IdProducto = producto.IdProducto;
            registro.IdSucursal = producto.IdSucursal;
            registro.PrecioVenta = nuevaVenta.Precio;
            registro.Cantidad = nuevaVenta.Cantidad;
            registro.Fecha = DateTime.Now;
            _context.RegistroVentas.Add(registro);
            return await _context.SaveChangesAsync() >= 0;
        }

        /// <summary>
        /// Obtener las ventas registradas por productos.
        /// </summary>
        /// <param name="idProducto">Identificador del producto</param>
        /// <returns>Objeto con la información de los productos y el total</returns>
        public async Task<TotalesViewModel> ObtenerVentasProductos(int? idProducto)
        {
            TotalesViewModel respuesta = new TotalesViewModel();

            if (idProducto != null)
            {
                var ventas = _context.RegistroVentas.Where(x => x.IdProducto == idProducto)
                    .Select(x => new VentasViewModel
                    {
                        Producto = x.IdProductoNavigation.Nombre,
                        FechaVenta = x.Fecha.ToString("dd/MM/yyyy"),
                        Id = x.Id,
                        PrecioVenta = x.PrecioVenta,
                        Sucursal = x.IdSucursalNavigation.Sucursal
                    }).ToList().OrderByDescending(x => x.FechaVenta);

                decimal totalVenta = ventas.Sum(x => x.PrecioVenta);

                respuesta.Total = totalVenta;
                respuesta.Ventas = (List<VentasViewModel>)ventas;
            }
            else
            {

                var ventas = _context.RegistroVentas.Select(x => new VentasViewModel
                {
                    Producto = x.IdProductoNavigation.Nombre,
                    FechaVenta = x.Fecha.ToString("dd/MM/yyyy"),
                    Id = x.Id,
                    PrecioVenta = x.PrecioVenta,
                    Sucursal = x.IdSucursalNavigation.Sucursal
                }).ToList().OrderByDescending(x => x.FechaVenta);

                decimal totalVenta = ventas.Sum(x => x.PrecioVenta);

                respuesta.Total = totalVenta;
                respuesta.Ventas = (List<VentasViewModel>)ventas;
            }

            return respuesta;
        }

        /// <summary>
        /// Obtener las ventas registradas por sucursal
        /// </summary>
        /// <param name="idSucursal">Identificador de la sucursal</param>
        /// <returns>Objeto con la información de los productos y el total</returns>
        public async Task<TotalesViewModel> ObtenerVentasSucursal(int? idSucursal)
        {
            TotalesViewModel respuesta = new TotalesViewModel();

            if (idSucursal != null)
            {
                var ventas = _context.RegistroVentas.Where(x => x.IdSucursal == idSucursal)
                    .Select(x => new VentasViewModel
                    {
                        Producto = x.IdProductoNavigation.Nombre,
                        FechaVenta = x.Fecha.ToString("dd/MM/yyyy"),
                        Id = x.Id,
                        PrecioVenta = x.PrecioVenta,
                        Sucursal = x.IdSucursalNavigation.Sucursal
                    }).ToList().OrderByDescending(x => x.FechaVenta);

                decimal totalVenta = ventas.Sum(x => x.PrecioVenta);

                respuesta.Total = totalVenta;
                respuesta.Ventas = (List<VentasViewModel>)ventas;
            }
            else
            {
                var ventas = _context.RegistroVentas.Select(x => new VentasViewModel
                {
                    Producto = x.IdProductoNavigation.Nombre,
                    FechaVenta = x.Fecha.ToString("dd/MM/yyyy"),
                    Id = x.Id,
                    PrecioVenta = x.PrecioVenta,
                    Sucursal = x.IdSucursalNavigation.Sucursal
                }).ToList().OrderByDescending(x => x.FechaVenta);

                decimal totalVenta = ventas.Sum(x => x.PrecioVenta);

                respuesta.Total = totalVenta;
                respuesta.Ventas = (List<VentasViewModel>)ventas;
            }

            return respuesta;
        }

    }
}
