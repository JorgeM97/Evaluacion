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
    public class ProductoRepository : IProductoRepository
    {
        private PruebasContext _context;
        public ProductoRepository(PruebasContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Actualizar la información de un producto de la base de datos.
        /// </summary>
        /// <param name="producto">Objeto con la información del producto</param>
        /// <returns>bool</returns>
        public async Task<bool> ActualizarProducto(ProductosViewModel producto)
        {
            var actualizarProducto = _context.Productos.Where(x => x.Id == producto.Id).FirstOrDefault();
            actualizarProducto.Nombre = producto.Nombre;
            actualizarProducto.CodigoBarras = producto.CodigoBarras;

            return await _context.SaveChangesAsync() >= 0;
        }

        /// <summary>
        /// Registrar la información de un nuevo producto en la base de datos.
        /// </summary>
        /// <param name="producto">Objeto con la información del producto</param>
        /// <returns>bool</returns>
        public async Task<bool> AgregarProductoNuevo(ProductosViewModel producto)
        {
            Productos nuevoProducto = new Productos();
            nuevoProducto.Nombre = producto.Nombre;
            nuevoProducto.CodigoBarras = producto.CodigoBarras;
            _context.Productos.Add(nuevoProducto);
            return await _context.SaveChangesAsync() >= 0;
        }

        /// <summary>
        /// Obtener el listado de productos registrados en la base de datos.
        /// </summary>
        /// <returns>Listado con la información de los productos</returns>
        public async Task<List<ProductosViewModel>> ObtenerProductos()
        {
            var productos = _context.Productos.Select(x => new ProductosViewModel
            {
                Id = x.Id,
                Nombre = x.Nombre,
                CodigoBarras = x.CodigoBarras
            }).ToList();
            return productos;
        }

        /// <summary>
        /// Validar si el código de barras ingresado ya se encuentra registrado
        /// </summary>
        /// <param name="CodigoBarras">Cádena del código de barras</param>
        /// <returns>bool</returns>
        public async Task<bool> ValidarCodigoBarrar(string CodigoBarras)
        {
            bool respuesta = true;

            if (CodigoBarras.All(char.IsDigit) && (_context.Productos.Any(x => x.CodigoBarras == CodigoBarras) == false))
                respuesta = false;
            return respuesta;
        }

        /// <summary>
        /// Validar si el nombre del producto ya se encuentra registrado
        /// </summary>
        /// <param name="Nombre">Nombre del producto</param>
        /// <returns>bool</returns>
        public async Task<bool> ValidarNombreProducto(string Nombre)
        {
            return _context.Productos.Any(x => x.Nombre == Nombre);
        }

        /// <summary>
        /// Validar si el producto exíste en la base de datos.
        /// </summary>
        /// <param name="idProducto">Identificador del producto</param>
        /// <returns>bool</returns>
        public async Task<bool> ValidarProducto(int idProducto)
        {
            return _context.Productos.Any(x => x.Id == idProducto);
        }

    }
}
