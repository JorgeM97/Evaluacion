﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion.Models.ViewModels
{
    /// <summary>
    /// Objeto para visualizar la información del inventario al que pertenece
    /// </summary>
    public class InventarioViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string CodigoBarras { get; set; }
        public string Sucursal { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public InventarioViewModel()
        {
            Id = 0;
            Nombre = string.Empty;
            CodigoBarras = string.Empty;
            Sucursal = string.Empty;
            Precio = 0;
            Cantidad = 0;
        }
    }
}
