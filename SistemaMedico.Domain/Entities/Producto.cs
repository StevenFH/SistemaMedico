using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaMedico.Domain.Entities
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }

        //Llave foránea y navegación
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; } = null!;
    }
}