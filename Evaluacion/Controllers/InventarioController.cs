using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Evaluacion.Helpers;
using Evaluacion.Models.ViewModels;
using Evaluacion.Helpers.Interfaces;
using System.IO;
using log4net;

namespace Evaluacion.Controladores
{
    [ApiController]
    [Route("[controller]")]
    public class InventarioController : ControllerBase
    {
        private IInventarioServices inventarioServices;
        private IProductoService productoService;
        public InventarioController(IInventarioServices inventario, IProductoService producto)
        {
            inventarioServices = inventario;
            productoService = producto;
            log4net.Util.LogLog.InternalDebugging = true;
            log4net.Config.XmlConfigurator.Configure(new FileInfo(@"~/log4net.config"));
        }

        private static ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Agregar una nueva sucursal a la base de datos
        /// </summary>
        /// <param name="sucursal">Objeto Sucursal</param>
        /// <returns>ServerResponse</returns>
        [HttpPost("AgregarSucursal")]
        [AllowAnonymous]
        public async Task<IActionResult> AgregarSucursal(SucursalesViewModel sucursal)
        {
            ServerResponseViewModel respuesta = new ServerResponseViewModel();
            try
            {
                respuesta = await inventarioServices.AgregarSucursal(sucursal);
            }
            catch (Exception e)
            {
                Logger.Info(e.Message.ToString());
                log4net.LogManager.Shutdown();
            }
            return Ok(respuesta);
        }

        /// <summary>
        /// Obtener el listado delas sucursales registradas
        /// </summary>
        /// <returns>Listado de sucursales</returns>
        [HttpGet("ObtenerSucursales")]
        [AllowAnonymous]
        public async Task<IActionResult> ObtenerSucursales()
        {
            List<SucursalesViewModel> respuesta = new List<SucursalesViewModel>();
            try
            {
                respuesta = await inventarioServices.ObtenerSucursales();
            }
            catch (Exception e)
            {
                Logger.Info(e.Message.ToString());
                log4net.LogManager.Shutdown();
            }
            return Ok(respuesta);
        }

        /// <summary>
        /// Obtener el listado de todos los prodcutos registrados por sucursal
        /// </summary>
        /// <param name="idSucursal">Identificador de la sucursal</param>
        /// <returns>Listado de productos por sucursales</returns>
        [HttpGet("ObtenerInventarioSucursal")]
        [AllowAnonymous]
        public async Task<IActionResult> ObtenerInventarioSucursal(int? idSucursal)
        {
            List<InventarioViewModel> respuesta = new List<InventarioViewModel>();
            try
            {
                respuesta = await inventarioServices.ObtenerInventario(idSucursal);
            }
            catch (Exception e)
            {
                Logger.Info(e.Message.ToString());
                log4net.LogManager.Shutdown();
            }
            return Ok(respuesta);
        }

        /// <summary>
        /// Revisar la existencia de un producto en las sucursales
        /// </summary>
        /// <param name="idProducto">Identificador del producto</param>
        /// <param name="idSucursal">Identificador de la sucursal</param>
        /// <returns>Listado de sucursales</returns>
        [HttpGet("ObtenerProductosPorSucursal")]
        [AllowAnonymous]
        public async Task<IActionResult> ObtenerProductoSucursal(int? idProducto, int? idSucursal)
        {
            List<InventarioViewModel> respuesta = new List<InventarioViewModel>();
            try
            {
                respuesta = await inventarioServices.ObtenerProductosSucursal((int)idProducto, (int)idSucursal);
            }
            catch (Exception e)
            {
                Logger.Info(e.Message.ToString());
                log4net.LogManager.Shutdown();
            }
            return Ok(respuesta);
        }

        /// <summary>
        /// Agregar un producto al inventario de una sucursal
        /// </summary>
        /// <param name="agregaraInventario">Objeto con la información de un producto</param>
        /// <returns></returns>
        [HttpPost("AgregarProductoAInventario")]
        [AllowAnonymous]
        public async Task<IActionResult> AgregarProductoInventario(ProductoInventarioViewModel agregaraInventario)
        {
            ServerResponseViewModel respuesta = new ServerResponseViewModel();
            try
            {
              respuesta = await inventarioServices.AgregarProductoInventario(agregaraInventario);
            }
            catch (Exception e)
            {
                Logger.Info(e.Message.ToString());
                log4net.LogManager.Shutdown();
            }
            return Ok(respuesta);
        }

        /// <summary>
        /// Actualizar el inventario de una sucursal
        /// </summary>
        /// <param name="actualizaraInventario">Objeto con la información de un producto</param>
        /// <returns></returns>
        [HttpPut("ActualizarProductoEnInventario")]
        [AllowAnonymous]
        public async Task<IActionResult> ActualizarProductoInventario(ProductoInventarioViewModel actualizaraInventario)
        {
            ServerResponseViewModel respuesta = new ServerResponseViewModel();
            try
            {
                respuesta = await inventarioServices.ActualziarProductoInventario(actualizaraInventario);
            }
            catch (Exception e)
            {
                Logger.Info(e.Message.ToString());
                log4net.LogManager.Shutdown();
            }
            return Ok(respuesta);
        }

        /// <summary>
        /// Eliminar producto de inventario
        /// </summary>
        /// <param name="idProducto">Identificador del producto</param>
        /// <returns></returns>
        [HttpDelete("EliminarProductoDeInventario")]
        [AllowAnonymous]
        public async Task<IActionResult> EliminarInventario(int? idProducto)
        {
            ServerResponseViewModel respuesta = new ServerResponseViewModel();
            try
            {
               respuesta = await inventarioServices.EliminarProductoInventario(idProducto);
            }
            catch (Exception e)
            {
                Logger.Info(e.Message.ToString());
                log4net.LogManager.Shutdown();
            }
            return Ok(respuesta);
        }   
    }
}
