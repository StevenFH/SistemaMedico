using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SistemaMedico.Application.DTOs
{
    public class ProductoDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        [Range(0.01, 1000000, ErrorMessage = "El precio debe ser mayor a cero")]
        public decimal Precio { get; set; }

        [Range(0, 100000, ErrorMessage = "La cantidad no puede ser negativa")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una categoría")]
        [Display(Name = "Categoría")]
        public int CategoriaId { get; set; }

        //Propiedad extra para mostrar el nombre de la categoría en las tablas de la vista
        public string? CategoriaNombre { get; set; }
    }
}