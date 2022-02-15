using Evaluacion.Helpers;
using Evaluacion.Helpers.Interfaces;
using Evaluacion.Modelos.Context;
using Evaluacion.Models.ViewModels;
using Evaluacion.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion.Services
{
    public class VentasService : IVentasService
    {
        private PruebasContext _context;
        private IInventarioRepository _inventarioRepository;
        private IRegistroErroesRepository _errores;
        private IProductoRepository _productoRepository;
        private IVentasRepository _ventasRepository;
        public VentasService(PruebasContext context, IInventarioRepository inventarioRepository, IRegistroErroesRepository errores, IProductoRepository productoRepository, IVentasRepository ventasRepository)
        {
            _context = context;
            _inventarioRepository = inventarioRepository;
            _errores = errores;
            _productoRepository = productoRepository;
            _ventasRepository = ventasRepository;
        }
        public async Task<TotalesViewModel> ObtenerVentasProductos(int? idProducto)
        {
            TotalesViewModel respuesta = new TotalesViewModel();

            try
            {
                respuesta = await _ventasRepository.ObtenerVentasProductos(idProducto);
            }
            catch (Exception e)
            {
                ErrorViewModel error = new ErrorViewModel();
                error.Mensaje = e.Message;
                error.Excepcion = e.InnerException != null ? e.InnerException.Message : string.Empty;
                //error.Codigo = e.
                error.Operacion = e.InnerException != null ? e.InnerException.TargetSite.Name : string.Empty;
                await _errores.RegistrarError(error);
            }
            return respuesta;
        }

        public async Task<TotalesViewModel> ObtenerVentasSucursal(int? idSucursal)
        {
            TotalesViewModel respuesta = new TotalesViewModel();

            try
            {
                respuesta = await _ventasRepository.ObtenerVentasSucursal(idSucursal);
            }
            catch (Exception e)
            {
                ErrorViewModel error = new ErrorViewModel();
                error.Mensaje = e.Message;
                error.Excepcion = e.InnerException != null ? e.InnerException.Message : string.Empty;
                //error.Codigo = e.
                error.Operacion = e.InnerException != null ? e.InnerException.TargetSite.Name : string.Empty;
                await _errores.RegistrarError(error);
            }
            return respuesta;
        }

        public async Task<ServerResponseViewModel> RegistrarVenta(NuevaVentaViewModel nuevaVenta)
        {
            ServerResponseViewModel respuesta = new ServerResponseViewModel();

            try
            {

                if (await _inventarioRepository.ValidarProductoInventario(nuevaVenta.IdInventario) != true)
                {
                    respuesta.Succeedded = false;
                    respuesta.Mesagge = Enumeratos.Errores.NO_EXISTE_INVENTARIO_ID.GetDescription();
                    respuesta.Code = Convert.ToInt32(Enumeratos.Errores.NO_EXISTE_INVENTARIO_ID);
                    respuesta.Header = "No hay ningún producto con ese ID en el inventario.";
                    return respuesta;
                }
                else if (nuevaVenta.Precio <= 0)
                {
                    respuesta.Succeedded = false;
                    respuesta.Mesagge = Enumeratos.Errores.PRECIO_INVALIDO.GetDescription();
                    respuesta.Code = Convert.ToInt32(Enumeratos.Errores.PRECIO_INVALIDO);
                    respuesta.Header = "El precio debe de ser mayor a 0.";
                    return respuesta;
                }
                else if (nuevaVenta.Cantidad <= 0)
                {
                    respuesta.Succeedded = false;
                    respuesta.Mesagge = Enumeratos.Errores.CANTIDAD_INVALIDA.GetDescription();
                    respuesta.Code = Convert.ToInt32(Enumeratos.Errores.CANTIDAD_INVALIDA);
                    respuesta.Header = "El precio debe de ser mayor a 0.";
                    return respuesta;
                }
                else
                {
                    respuesta.Succeedded = await _ventasRepository.RegistrarVenta(nuevaVenta);
                    if (respuesta.Succeedded == true)
                    {
                        respuesta.Mesagge = Enumeratos.Exito.VENTA_REGISTRADA.GetDescription();
                        respuesta.Code = Convert.ToInt32(Enumeratos.Exito.VENTA_REGISTRADA);
                        respuesta.Header = "Venta guardada.";
                        return respuesta;
                    }
                    else
                    {
                        respuesta.Mesagge = Enumeratos.Errores.ERROR_VENTA.GetDescription();
                        respuesta.Code = Convert.ToInt32(Enumeratos.Errores.ERROR_VENTA);
                        respuesta.Header = "Error al generar la venta.";
                        return respuesta;
                    }
                }
            }
            catch (Exception e)
            {
                ErrorViewModel error = new ErrorViewModel();
                error.Mensaje = e.Message;
                error.Excepcion = e.InnerException != null ? e.InnerException.Message : string.Empty;
                //error.Codigo = e.
                error.Operacion = e.InnerException != null ? e.InnerException.TargetSite.Name : string.Empty;
                await _errores.RegistrarError(error);

                respuesta.Mesagge = "No se pudo eliminar el producto por un error interno.";
            }

            return respuesta;

        }
    }
}
