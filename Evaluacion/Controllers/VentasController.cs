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
    public class VentasController : ControllerBase
    {
        private IInventarioServices inventarioServices;
        private IProductoService productoService;
        private IVentasService ventasService;
        public VentasController(IInventarioServices inventario, IProductoService producto, IVentasService ventas)
        {
            inventarioServices = inventario;
            productoService = producto;
            ventasService = ventas;
            log4net.Util.LogLog.InternalDebugging = true;
            log4net.Config.XmlConfigurator.Configure(new FileInfo(@"~/log4net.config"));
        }

        private static ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Registrar la venta de un producto
        /// </summary>
        /// <param name="idProductoInventario">Identificador producto</param>
        /// <returns></returns>
        [HttpPost("RegistrarVenta")]
        [AllowAnonymous]
        public async Task<IActionResult> RegistrarVenta(NuevaVentaViewModel nuevaVenta)
        {
            ServerResponseViewModel respuesta = new ServerResponseViewModel();
            try
            {
                respuesta = await ventasService.RegistrarVenta(nuevaVenta);
            }
            catch (Exception e)
            {
                Logger.Info(e.Message.ToString());
                log4net.LogManager.Shutdown();
            }
            return Ok(respuesta);
        }
        /// <summary>
        /// Obtener las ventas por sucursal
        /// </summary>
        /// <param name="idSucursal">Identificador de la sucursal</param>
        /// <returns></returns>
        [HttpGet("ObtenerVentasPorSucursales")]
        [AllowAnonymous]
        public async Task<IActionResult> ObtenerVentasSucursal(int? idSucursal)
        {
            TotalesViewModel respuesta = new TotalesViewModel();
            try
            {
                respuesta = await ventasService.ObtenerVentasSucursal((int)idSucursal);
            }
            catch (Exception e)
            {
                Logger.Info(e.Message.ToString());
                log4net.LogManager.Shutdown();
            }
            return Ok(respuesta);
        }

        /// <summary>
        /// Obtener las ventas por producto
        /// </summary>
        /// <param name="idProducto">Identficador de productos</param>
        /// <returns></returns>
        [HttpGet("ObtenerVentasPorProductos")]
        [AllowAnonymous]
        public async Task<IActionResult> ObtenerVentasProducto(int? idProducto)
        {
            TotalesViewModel respuesta = new TotalesViewModel();
            try
            {
                 respuesta = await ventasService.ObtenerVentasProductos((int)idProducto);
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
