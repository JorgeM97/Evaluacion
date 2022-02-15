using Evaluacion.Helpers.Interfaces;
using Evaluacion.Models.ViewModels;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductosController : ControllerBase
    {
        private IInventarioServices inventarioServices;
        public IProductoService productoService;

        public ProductosController(IInventarioServices inventario, IProductoService producto)
        {
            inventarioServices = inventario;
            productoService = producto;
            log4net.Util.LogLog.InternalDebugging = true;
            log4net.Config.XmlConfigurator.Configure(new FileInfo(@"~/log4net.config"));
        }

        private static ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Obtener el listado de todos los productos registrados
        /// </summary>
        /// <returns>Listado de productos</returns>
        [HttpGet("ObtenerProductos")]
        [AllowAnonymous]
        public async Task<IActionResult> ObtenerProductos()
        {
            List<ProductosViewModel> respuesta = new List<ProductosViewModel>();
            try
            {
                respuesta = await productoService.ObtenerProductos();
            }
            catch (Exception e)
            {
                Logger.Info(e.Message.ToString());
                log4net.LogManager.Shutdown();
            }
            return Ok(respuesta);

        }

        /// <summary>
        /// Registrar un nuevo producto
        /// </summary>
        /// <param name="nuevoProducto">Objeto con la información del producto nuevo</param>
        /// <returns></returns>
        [HttpPost("AgregarNuevoProducto")]
        [AllowAnonymous]
        public async Task<IActionResult> AgregarProducto(ProductosViewModel nuevoProducto)
        {
            ServerResponseViewModel respuesta = new ServerResponseViewModel();
            try
            {
                respuesta = await productoService.AgregarProducto(nuevoProducto);
            }
            catch (Exception e)
            {
                Logger.Info(e.Message.ToString());
                log4net.LogManager.Shutdown();
            }
            return Ok(respuesta);
        }

        /// <summary>
        /// Actualizar la información de un producto
        /// </summary>
        /// <param name="actualizarProducto">Objeto con la información de productos</param>
        /// <returns></returns>
        [HttpPut("ActualizarProducto")]
        [AllowAnonymous]
        public async Task<IActionResult> ActualizarProducto(ProductosViewModel actualizarProducto)
        {
            ServerResponseViewModel respuesta = new ServerResponseViewModel();
            try
            {
                respuesta = await productoService.ActualizarProducto(actualizarProducto);
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
