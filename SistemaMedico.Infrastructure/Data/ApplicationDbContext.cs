using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SistemaMedico.Domain.Entities;

namespace SistemaMedico.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Configuración de precisión
            modelBuilder.Entity<Producto>()
                .Property(p => p.Precio)
                .HasPrecision(18, 2);

            //Configuración de relación
            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Productos)
                .HasForeignKey(p => p.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);

            // --- SEED DATA ---
            // 1. Semilla de Categorías
            modelBuilder.Entity<Categoria>().HasData(
                new Categoria { Id = 1, Nombre = "Analgésicos", Descripcion = "Medicamentos para aliviar el dolor" },
                new Categoria { Id = 2, Nombre = "Antibióticos", Descripcion = "Fármacos para tratar infecciones bacterianas" },
                new Categoria { Id = 3, Nombre = "Equipamiento Básico", Descripcion = "Insumos y herramientas de uso diario" }
            );

            // 2. Semilla de Productos Médicos (Precios actualizados a Colones)
            modelBuilder.Entity<Producto>().HasData(
                new Producto { Id = 1, Nombre = "Paracetamol 500mg", Descripcion = "Caja x 100 tabletas", Precio = 3500.00m, Cantidad = 200, CategoriaId = 1 },
                new Producto { Id = 2, Nombre = "Ibuprofeno 400mg", Descripcion = "Caja x 50 cápsulas", Precio = 4800.00m, Cantidad = 150, CategoriaId = 1 },
                new Producto { Id = 3, Nombre = "Amoxicilina 500mg", Descripcion = "Caja x 20 cápsulas", Precio = 6500.00m, Cantidad = 80, CategoriaId = 2 },
                new Producto { Id = 4, Nombre = "Azitromicina 250mg", Descripcion = "Caja x 5 tabletas", Precio = 7200.00m, Cantidad = 50, CategoriaId = 2 },
                new Producto { Id = 5, Nombre = "Termómetro Digital", Descripcion = "Medición rápida con alarma", Precio = 4500.00m, Cantidad = 30, CategoriaId = 3 },
                new Producto { Id = 6, Nombre = "Mascarillas KN95", Descripcion = "Caja x 50 unidades", Precio = 8500.00m, Cantidad = 500, CategoriaId = 3 }
            );
        }
    }
}