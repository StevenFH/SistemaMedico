using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaMedico.Domain.Entities
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;

        //Propiedad de navegación para Entity Framework
        public ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}