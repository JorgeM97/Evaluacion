using Evaluacion.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;
using Evaluacion.Modelos.Context;
using Evaluacion.Models.ViewModels;
using Evaluacion.Helpers.Interfaces;

namespace Evaluacion.Services
{
    public class InventarioServices : IInventarioServices
    {   
        private PruebasContext _context;
        private IInventarioRepository _inventarioRepository;
        private IRegistroErroesRepository _errores;
        private IProductoRepository _productoRepository;
        private IVentasRepository _ventasRepository;
        public InventarioServices(PruebasContext context, IInventarioRepository inventarioRepository, IRegistroErroesRepository errores, IProductoRepository productoRepository, IVentasRepository ventasRepository)
        {
            _context = context;
            _inventarioRepository = inventarioRepository;
            _errores = errores;
            _productoRepository = productoRepository;
            _ventasRepository = ventasRepository;
        }

        public async Task<ServerResponseViewModel> ActualziarProductoInventario(ProductoInventarioViewModel productoInventario)
        {
            ServerResponseViewModel respuesta = new ServerResponseViewModel();

            try
            {
                if (await _inventarioRepository.ValidarProductoInventario(productoInventario.Id) != true)
                {
                    respuesta.Succeedded = false;
                    respuesta.Mesagge = Enumeratos.Errores.NO_EXISTE_INVENTARIO_ID.GetDescription();
                    respuesta.Code = Convert.ToInt32(Enumeratos.Errores.NO_EXISTE_INVENTARIO_ID);
                    respuesta.Header = "No hay ningún producto con ese ID en el inventario.";
                    return respuesta;
                }
                else if (await _productoRepository.ValidarProducto(productoInventario.IdProducto) != true)
                {
                    respuesta.Succeedded = false;
                    respuesta.Mesagge = Enumeratos.Errores.NO_EXISTE_PRODCUTO_ID.GetDescription();
                    respuesta.Code = Convert.ToInt32(Enumeratos.Errores.NO_EXISTE_PRODCUTO_ID);
                    respuesta.Header = "No hay ningún producto con ese ID.";
                    return respuesta;
                }
                else if(await _inventarioRepository.ValidarSucursal(productoInventario.IdSucursal) != true)
                {
                    respuesta.Succeedded = false;
                    respuesta.Mesagge = Enumeratos.Errores.NO_EXISTE_SUCURSAL_ID.GetDescription();
                    respuesta.Code = Convert.ToInt32(Enumeratos.Errores.NO_EXISTE_SUCURSAL_ID);
                    respuesta.Header = "No hay ninguna sucursal con ese ID";
                    return respuesta;
                }
                else
                {
                    respuesta.Succeedded = await _inventarioRepository.ActualizarProductoInventario(productoInventario);
                    if (respuesta.Succeedded == true)
                    {
                        respuesta.Mesagge = Enumeratos.Exito.ACTUALIZACION_INVENTARIO.GetDescription();
                        respuesta.Code = Convert.ToInt32(Enumeratos.Exito.ACTUALIZACION_INVENTARIO);
                        respuesta.Header = "Se actualizo el producto del inventario.";
                    }
                    else
                    {
                        respuesta.Mesagge = Enumeratos.Errores.ERROR_ACTUALIZAR_PRODUCTO.GetDescription();
                        respuesta.Code = Convert.ToInt32(Enumeratos.Errores.ERROR_ACTUALIZAR_PRODUCTO);
                        respuesta.Header = "Ocurrio un error interno.";
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

                respuesta.Mesagge = "No se pudo realizar la actualización del inventario por un error interno.";
            }

            return respuesta;
        }

        public async Task <ServerResponseViewModel> AgregarProductoInventario(ProductoInventarioViewModel productoInventario)
        {
            ServerResponseViewModel respuesta = new ServerResponseViewModel();

            try {
              if (await _productoRepository.ValidarProducto(productoInventario.IdProducto) != true)
                {
                    respuesta.Succeedded = false;
                    respuesta.Mesagge = Enumeratos.Errores.NO_EXISTE_PRODCUTO_ID.GetDescription();
                    respuesta.Code = Convert.ToInt32(Enumeratos.Errores.NO_EXISTE_PRODCUTO_ID);
                    respuesta.Header = "No hay ningún producto con ese ID.";
                    return respuesta;
                }
                else if (await _inventarioRepository.ValidarSucursal(productoInventario.IdSucursal) != true)
                {
                    respuesta.Succeedded = false;
                    respuesta.Mesagge = Enumeratos.Errores.NO_EXISTE_SUCURSAL_ID.GetDescription();
                    respuesta.Code = Convert.ToInt32(Enumeratos.Errores.NO_EXISTE_SUCURSAL_ID);
                    respuesta.Header = "No hay ninguna sucursal con ese ID";
                    return respuesta;
                }
                else
                {
                    respuesta.Succeedded = await _inventarioRepository.ActualizarProductoInventario(productoInventario);
                    if (respuesta.Succeedded == true)
                    {
                        respuesta.Mesagge = Enumeratos.Exito.PRODUCTO_NUEVO_INVENTARIO_ANADIDO.GetDescription();
                        respuesta.Code = Convert.ToInt32(Enumeratos.Exito.PRODUCTO_NUEVO_INVENTARIO_ANADIDO);
                        respuesta.Header = "Se guardo el producto en el inventario.";
                    }
                    else
                    {
                        respuesta.Mesagge = Enumeratos.Errores.ERROR_NUEVO_PRODUCTO_INVENTARIO.GetDescription();
                        respuesta.Code = Convert.ToInt32(Enumeratos.Errores.ERROR_NUEVO_PRODUCTO_INVENTARIO);
                        respuesta.Header = "Ocurrio un error interno al guardar el producto.";
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

                respuesta.Mesagge = "No se pudo guardar el nuevo producto por un error interno.";
            }
            return respuesta;
        }

        public async Task<ServerResponseViewModel> AgregarSucursal(SucursalesViewModel sucursal)
        {
            ServerResponseViewModel respuesta = new ServerResponseViewModel();

            try
            {
                if (string.IsNullOrEmpty(sucursal.Sucursal) == true)
                {
                    respuesta.Succeedded = false;
                    respuesta.Mesagge = Enumeratos.Errores.NOMBRE_SUCURSAL_VACIO.GetDescription();
                    respuesta.Code = Convert.ToInt32(Enumeratos.Errores.NOMBRE_SUCURSAL_VACIO);
                    respuesta.Header = "Nombre de la sucursal vacío.";
                    return respuesta;
                }
                if (await _inventarioRepository.ValidarNombreSucursal(sucursal.Sucursal) == true)
                {
                    respuesta.Succeedded = false;
                    respuesta.Mesagge = Enumeratos.Errores.ERROR_NOMBRE_SUCURSAL.GetDescription();
                    respuesta.Code = Convert.ToInt32(Enumeratos.Errores.ERROR_NOMBRE_SUCURSAL);
                    respuesta.Header = "Nombre de la sucursal duplicado.";
                    return respuesta;
                }
                else
                {
                    respuesta.Succeedded = await _inventarioRepository.AgregarSucursal(sucursal);
                    if (respuesta.Succeedded == true)
                    {
                        respuesta.Mesagge = Enumeratos.Exito.SUCURSAL_REGISTRADA.GetDescription();
                        respuesta.Code = Convert.ToInt32(Enumeratos.Exito.SUCURSAL_REGISTRADA);
                        respuesta.Header = "Nueva sucursal registrada.";
                    }
                    else {
                        respuesta.Mesagge = Enumeratos.Errores.ERROR_NUEVA_SUCURSAL.GetDescription();
                        respuesta.Code = Convert.ToInt32(Enumeratos.Errores.ERROR_NUEVA_SUCURSAL);
                        respuesta.Header = "Error al guardar la sucursal.";
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

                respuesta.Mesagge = "No se pudo registrar la nueva sucursal.";
            }

            return respuesta;
        }

        public async Task<ServerResponseViewModel> EliminarProductoInventario(int? idProducto)
        {
            ServerResponseViewModel respuesta = new ServerResponseViewModel();

            try {
                if (await _inventarioRepository.ValidarProductoInventario((int)idProducto) != true)
                {
                    respuesta.Succeedded = false;
                    respuesta.Mesagge = Enumeratos.Errores.NO_EXISTE_INVENTARIO_ID.GetDescription();
                    respuesta.Code = Convert.ToInt32(Enumeratos.Errores.NO_EXISTE_INVENTARIO_ID);
                    respuesta.Header = "No hay ningún producto con ese ID en el inventario.";
                    return respuesta;
                }
                else {
                    respuesta.Succeedded = await _inventarioRepository.EliminarProductoInventario((int)idProducto);
                    if (respuesta.Succeedded == true)
                    {
                        respuesta.Mesagge = Enumeratos.Exito.PRODUCTO_ELIMINADO.GetDescription();
                        respuesta.Code = Convert.ToInt32(Enumeratos.Exito.PRODUCTO_ELIMINADO);
                        respuesta.Header = "Se elimino el producto del inventario.";
                    }
                    else
                    {
                        respuesta.Mesagge = Enumeratos.Errores.ERROR_ELIMINANDO_PRODUCTO.GetDescription();
                        respuesta.Code = Convert.ToInt32(Enumeratos.Errores.ERROR_ELIMINANDO_PRODUCTO);
                        respuesta.Header = "Ocurrio un error al eliminar el producto del inventario.";
                    }
                }
            }
            catch (Exception e) {
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

        public async Task<List<InventarioViewModel>> ObtenerInventario(int? idSucursal)
        {
            List<InventarioViewModel> respuesta = new List<InventarioViewModel>();

            try
            {
                if (idSucursal != null)
                {
                    if (await _inventarioRepository.ValidarSucursal((int)idSucursal) == true)
                        return respuesta;
                    else
                        respuesta = await _inventarioRepository.ObtenerInventario((int)idSucursal);
                }
                else
                {
                    respuesta = await _inventarioRepository.ObtenerInventario(idSucursal);
                }
            }
            catch (Exception e) {
                ErrorViewModel error = new ErrorViewModel();
                error.Mensaje = e.Message;
                error.Excepcion = e.InnerException != null ? e.InnerException.Message : string.Empty;
                //error.Codigo = e.
                error.Operacion = e.InnerException != null ? e.InnerException.TargetSite.Name : string.Empty;
                await _errores.RegistrarError(error);

                return respuesta;
            }
            return respuesta;
        }

        public async Task<List<InventarioViewModel>> ObtenerProductosSucursal(int? idProducto, int? idSucursal)
        {
            List<InventarioViewModel> respuesta = new List<InventarioViewModel>();

            try
            {
                if (idProducto != null && await _productoRepository.ValidarProducto((int)idProducto) == true)
                {
                    respuesta = await _inventarioRepository.ObtenerProductosSucursal((int)idProducto, idSucursal);
                }
                else
                {
                    return respuesta;
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

                return respuesta;
            }

            return respuesta;
        }

        public async Task<List<SucursalesViewModel>> ObtenerSucursales()
        {
            List<SucursalesViewModel> respuesta = new List<SucursalesViewModel>();

            try {
                respuesta = await _inventarioRepository.ObtenerSucursales();
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
    }
}
