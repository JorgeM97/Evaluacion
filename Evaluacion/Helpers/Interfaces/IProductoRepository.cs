using Evaluacion.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion.Helpers.Interfaces
{
    public interface IProductoRepository
    {
        Task<List<ProductosViewModel>> ObtenerProductos();
        Task<bool> ValidarProducto(int idProducto);
        Task<bool> ValidarNombreProducto(string Nombre);
        Task<bool> ValidarCodigoBarrar(string CodigoBarras);
        Task<bool> ActualizarProducto(ProductosViewModel producto);
        Task<bool> AgregarProductoNuevo(ProductosViewModel producto);
    }
}
