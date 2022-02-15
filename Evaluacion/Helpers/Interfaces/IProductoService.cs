using Evaluacion.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion.Helpers.Interfaces
{
    public interface IProductoService
    {
        Task<List<ProductosViewModel>> ObtenerProductos();
        Task<ServerResponseViewModel> AgregarProducto(ProductosViewModel nuevoProducto);
        Task<ServerResponseViewModel> ActualizarProducto(ProductosViewModel Producto);
    }
}
