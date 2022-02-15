using Evaluacion.Helpers;
using Evaluacion.Helpers.Interfaces;
using Evaluacion.Modelos.Context;
using Evaluacion.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion.Services
{
    public class ProductoService : IProductoService
    {
        private PruebasContext _context;
        private IInventarioRepository _inventarioRepository;
        private IRegistroErroesRepository _errores;
        private IProductoRepository _productoRepository;
        public ProductoService(PruebasContext context, IInventarioRepository InventarioRepository, IRegistroErroesRepository errores, IProductoRepository ProductoRepository)
        {
            _context = context;
            _inventarioRepository = InventarioRepository;
            _errores = errores;
            _productoRepository = ProductoRepository;
        }
        public async Task<ServerResponseViewModel> ActualizarProducto(ProductosViewModel Producto)
        {
            ServerResponseViewModel respuesta = new ServerResponseViewModel();

            try
            {
                if (await _productoRepository.ValidarProducto(Producto.Id) != true)
                {
                    respuesta.Succeedded = false;
                    respuesta.Mesagge = Enumeratos.Errores.NO_EXISTE_PRODCUTO_ID.GetDescription();
                    respuesta.Code = Convert.ToInt32(Enumeratos.Errores.NO_EXISTE_PRODCUTO_ID);
                    respuesta.Header = "No hay ningún producto con ese ID.";
                    return respuesta;
                }
                else if (string.IsNullOrEmpty(Producto.Nombre))
                {
                    respuesta.Succeedded = false;
                    respuesta.Mesagge = Enumeratos.Errores.ERROR_NOMBRE_PRODUCTO_VACIO.GetDescription();
                    respuesta.Code = Convert.ToInt32(Enumeratos.Errores.ERROR_NOMBRE_PRODUCTO_VACIO);
                    respuesta.Header = "Nombre del producto vacío.";
                    return respuesta;
                }
                else if (await _productoRepository.ValidarNombreProducto(Producto.Nombre) == true)
                {
                    respuesta.Succeedded = false;
                    respuesta.Mesagge = Enumeratos.Errores.NOMBRE_PRODUCTO_DUPLICADO.GetDescription();
                    respuesta.Code = Convert.ToInt32(Enumeratos.Errores.NOMBRE_PRODUCTO_DUPLICADO);
                    respuesta.Header = "Ya hay un producto con ese nombre.";
                    return respuesta;
                }
                else if (await _productoRepository.ValidarCodigoBarrar(Producto.CodigoBarras) == true)
                {
                    respuesta.Succeedded = false;
                    respuesta.Mesagge = Enumeratos.Errores.CODIGO_BARRAS_ERROR.GetDescription();
                    respuesta.Code = Convert.ToInt32(Enumeratos.Errores.CODIGO_BARRAS_ERROR);
                    respuesta.Header = "Se encontro un detalle con el código de barras.";
                    return respuesta;
                }
                else
                {
                    respuesta.Succeedded = await _productoRepository.ActualizarProducto(Producto);
                    if (respuesta.Succeedded == true)
                    {
                        respuesta.Mesagge = Enumeratos.Exito.ACTUALIZACION_PRODICTO.GetDescription();
                        respuesta.Code = Convert.ToInt32(Enumeratos.Exito.ACTUALIZACION_PRODICTO);
                        respuesta.Header = "Se actualizo el producto de manera correcta.";
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

                respuesta.Mesagge = "No se pudo realizar la actualización del producto por un error interno.";
            }

            return respuesta;
        }

        public async Task<ServerResponseViewModel> AgregarProducto(ProductosViewModel nuevoProducto)
        {
            ServerResponseViewModel respuesta = new ServerResponseViewModel();

            try
            {
                if (string.IsNullOrEmpty(nuevoProducto.Nombre))
                {
                    respuesta.Succeedded = false;
                    respuesta.Mesagge = Enumeratos.Errores.ERROR_NOMBRE_PRODUCTO_VACIO.GetDescription();
                    respuesta.Code = Convert.ToInt32(Enumeratos.Errores.ERROR_NOMBRE_PRODUCTO_VACIO);
                    respuesta.Header = "Nombre del producto vacío.";
                    return respuesta;
                }
                else if (await _productoRepository.ValidarNombreProducto(nuevoProducto.Nombre) == true)
                {
                    respuesta.Succeedded = false;
                    respuesta.Mesagge = Enumeratos.Errores.NOMBRE_PRODUCTO_DUPLICADO.GetDescription();
                    respuesta.Code = Convert.ToInt32(Enumeratos.Errores.NOMBRE_PRODUCTO_DUPLICADO);
                    respuesta.Header = "Ya hay un producto con ese nombre.";
                    return respuesta;
                }
                else if (await _productoRepository.ValidarCodigoBarrar(nuevoProducto.CodigoBarras) == true)
                {
                    respuesta.Succeedded = false;
                    respuesta.Mesagge = Enumeratos.Errores.CODIGO_BARRAS_ERROR.GetDescription();
                    respuesta.Code = Convert.ToInt32(Enumeratos.Errores.CODIGO_BARRAS_ERROR);
                    respuesta.Header = "Se encontro un detalle con el código de barras.";
                    return respuesta;
                }
                else
                {
                    respuesta.Succeedded = await _productoRepository.AgregarProductoNuevo(nuevoProducto);
                    if (respuesta.Succeedded == true)
                    {
                        respuesta.Mesagge = Enumeratos.Exito.PRODUCTO_NUEVO_ANADIDO.GetDescription();
                        respuesta.Code = Convert.ToInt32(Enumeratos.Exito.PRODUCTO_NUEVO_ANADIDO);
                        respuesta.Header = "Se guardo el nuevo producto.";
                    }
                    else
                    {
                        respuesta.Mesagge = Enumeratos.Errores.ERROR_NUEVO_PRODUCTO.GetDescription();
                        respuesta.Code = Convert.ToInt32(Enumeratos.Errores.ERROR_NUEVO_PRODUCTO);
                        respuesta.Header = "Error al crear el nuevo producto.";
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

                respuesta.Mesagge = "No se pudo guardar del producto por un error interno.";
            }

            return respuesta;
        }

        public async Task<List<ProductosViewModel>> ObtenerProductos()
        {
            List<ProductosViewModel> respuesta = new List<ProductosViewModel>();
            try
            {
                respuesta = await _productoRepository.ObtenerProductos();
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
    }
}
