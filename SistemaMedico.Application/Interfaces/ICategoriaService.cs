using System;
using System.Collections.Generic;
using System.Text;
using SistemaMedico.Application.DTOs;

namespace SistemaMedico.Application.Interfaces
{
    public interface ICategoriaService
    {
        Task<IEnumerable<CategoriaDTO>> GetAllAsync();
        Task<CategoriaDTO?> GetByIdAsync(int id);
        Task CreateAsync(CategoriaDTO categoriaDto);
        Task UpdateAsync(CategoriaDTO categoriaDto);
        Task DeleteAsync(int id);
    }
}