using System;
using System.Collections.Generic;
using System.Text;
using SistemaMedico.Application.DTOs;

namespace SistemaMedico.Application.Interfaces
{
    public interface IProductoService
    {
        Task<IEnumerable<ProductoDTO>> GetAllAsync();
        Task<ProductoDTO?> GetByIdAsync(int id);
        Task CreateAsync(ProductoDTO productoDto);
        Task UpdateAsync(ProductoDTO productoDto);
        Task DeleteAsync(int id);
    }
}