using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Evaluacion.Models.Entities;
using Microsoft.Extensions.Configuration;

namespace Evaluacion.Modelos.Context
{
    /// <summary>
    /// Clase para declarar el contexto de mi base de datos de prueba y los objetos a utilizar.
    /// </summary>
    public class PruebasContext : DbContext
    {
        public PruebasContext(DbContextOptions<PruebasContext> options) : base(options) 
        { }
        public DbSet<Inventario> Inventario { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<RegistroVentas> RegistroVentas { get; set; }
        public DbSet<Sucursales> Sucursales { get; set; }
        public DbSet<RegistroErrores> RegistroErrores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-CLM3V11; Initial Catalog=Pruebas; User ID=JorgePruebas; Password=1Jorge97");
            }
        }

        /// <summary>
        /// Configuración basica del modelo de base de datos y declaración de llaves foraneas para la consulta de información.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inventario>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.Cantidad).HasColumnName("Cantidad");
                entity.Property(e => e.Precio).HasColumnName("Precio");
                entity.Property(e => e.IdSucursal).HasColumnName("IdSucursal");
                entity.Property(e => e.IdProducto).HasColumnName("IdProducto");
                
                entity.HasOne(d => d.IdProductoNavigation)
                           .WithMany(p => p.Inventario)
                            .HasForeignKey(d => d.IdProducto)
                            .HasConstraintName("FK_IdProductos");

                entity.HasOne(d => d.IdSucursalNavigation)
                            .WithMany(p => p.Inventario)
                            .HasForeignKey(d => d.IdSucursal)
                            .HasConstraintName("FK_IdSucursal");
            });

            modelBuilder.Entity<Productos>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.Nombre).HasColumnName("Nombre");
                entity.Property(e => e.CodigoBarras).HasColumnName("CodigoBarras");
            });

            modelBuilder.Entity<Sucursales>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.Sucursal).HasColumnName("Sucursal");
            });

            modelBuilder.Entity<RegistroVentas>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.PrecioVenta).HasColumnName("PrecioVenta");
                entity.Property(e => e.Fecha).HasColumnName("Fecha");
                entity.Property(e => e.Cantidad).HasColumnName("Cantidad");

                entity.HasOne(d => d.IdProductoNavigation)
                          .WithMany(p => p.RegistroVentas)
                           .HasForeignKey(d => d.IdProducto)
                           .HasConstraintName("FK__RegistroV__IdPro");

                entity.HasOne(d => d.IdSucursalNavigation)
                            .WithMany(p => p.RegistroVentas)
                            .HasForeignKey(d => d.IdSucursal)
                            .HasConstraintName("FK__RegistroV__IdSuc");
            });

            modelBuilder.Entity<RegistroErrores>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.FechaError).HasColumnName("FechaError");
                entity.Property(e => e.Excepcion).HasColumnName("Excepcion");
                entity.Property(e => e.Operacion).HasColumnName("Operacion");
                entity.Property(e => e.Codigo).HasColumnName("Codigo");
                entity.Property(e => e.Mensaje).HasColumnName("Mensaje");
            });
        }        
    }
}
