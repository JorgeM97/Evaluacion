using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion.Helpers
{
    public class Enumeratos
    {
        /// <summary>
        /// Codigos de error para las respuestas de la aplicación
        /// </summary>
        public enum Errores 
        {
            [Description("No existe ningún producto con ese Id.")]
            NO_EXISTE_PRODCUTO_ID = 1001,
            [Description("Ya existe un producto con ese nombre.")]
            NOMBRE_PRODUCTO_DUPLICADO = 1002,
            [Description("El código de barras solo puede contener numeros o ya existe un producto con el mismo código.")]
            CODIGO_BARRAS_ERROR = 1003,
            [Description("Error interno al actualizar el producto.")]
            ERROR_ACTUALIZAR_PRODUCTO = 1004,
            [Description("No existe ninguna sucursal con ese Id.")]
            NO_EXISTE_SUCURSAL_ID = 1005,
            [Description("No existe ningún producto con ese Id en el inventario.")]
            NO_EXISTE_INVENTARIO_ID = 1006,
            [Description("Error al crear el nuevo producto")]
            ERROR_NUEVO_PRODUCTO = 1007,
            [Description("Error al cargar producto al inventario")]
            ERROR_NUEVO_PRODUCTO_INVENTARIO = 1008,
            [Description("No se pudo eliminar el producto del inventario")]
            ERROR_ELIMINANDO_PRODUCTO = 1009,
            [Description("No se pudo realizar el registro de la venta.")]
            ERROR_VENTA = 1010,
            [Description("El nombre del producto no puede ir vacio.")]
            ERROR_NOMBRE_PRODUCTO = 1011,
            [Description("El nombre de la sucursal ya se encuentra registrado.")]
            ERROR_NOMBRE_SUCURSAL = 1012,
            [Description("EL nombre de la sucursal no puede ir vacío.")]
            NOMBRE_SUCURSAL_VACIO = 1013,
            [Description("Error interno al guardar la nueva sucursal.")]
            ERROR_NUEVA_SUCURSAL = 1014,
            [Description("El nombre del producto no puede ir vacío.")]
            ERROR_NOMBRE_PRODUCTO_VACIO,
            [Description("El precio de la venta debe de ser mayor a 0")]
            PRECIO_INVALIDO,
            [Description("La cantidad de productos debe de ser mayor a 0")]
            CANTIDAD_INVALIDA
        }

        //Codigos de exito para las respuestas de la aplicacion 
        public enum Exito
        {
            [Description("Se actualizo el archivo correctamente.")]
            ACTUALIZACION_PRODICTO = 2002,
            [Description("Se actualizo el producto del inventario.")]
            ACTUALIZACION_INVENTARIO = 2001,
            [Description("Se ha guardado el nuevo producto.")]
            PRODUCTO_NUEVO_ANADIDO = 2003,
            [Description("Se ha guardado el producto en el inventario.")]
            PRODUCTO_NUEVO_INVENTARIO_ANADIDO = 2004,
            [Description("Se elimino el producto del inventario.")]
            PRODUCTO_ELIMINADO = 2005,
            [Description("Se registro la venta correctamente.")]
            VENTA_REGISTRADA = 2006,
            [Description("Se registro la nueva sucursal.")]
            SUCURSAL_REGISTRADA = 2007
        }
    }
}
