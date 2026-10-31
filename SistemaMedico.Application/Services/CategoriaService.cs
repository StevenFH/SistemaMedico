using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using SistemaMedico.Application.DTOs;
using SistemaMedico.Application.Interfaces;
using SistemaMedico.Domain.Entities;
using SistemaMedico.Domain.Interfaces;

namespace SistemaMedico.Application.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly IGenericRepository<Categoria> _categoriaRepository;
        private readonly IGenericRepository<Producto> _productoRepository;
        private readonly IMapper _mapper;

        public CategoriaService(
            IGenericRepository<Categoria> categoriaRepository,
            IGenericRepository<Producto> productoRepository,
            IMapper mapper)
        {
            _categoriaRepository = categoriaRepository;
            _productoRepository = productoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoriaDTO>> GetAllAsync()
        {
            var categorias = await _categoriaRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoriaDTO>>(categorias);
        }

        public async Task<CategoriaDTO?> GetByIdAsync(int id)
        {
            var categoria = await _categoriaRepository.GetByIdAsync(id);
            return categoria != null ? _mapper.Map<CategoriaDTO>(categoria) : null;
        }

        public async Task CreateAsync(CategoriaDTO categoriaDto)
        {
            var categoria = _mapper.Map<Categoria>(categoriaDto);
            await _categoriaRepository.AddAsync(categoria);
            await _categoriaRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(CategoriaDTO categoriaDto)
        {
            var categoria = _mapper.Map<Categoria>(categoriaDto);
            _categoriaRepository.Update(categoria);
            await _categoriaRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            //Regla de negocio: Validar si existen productos asignados a esta categoría
            var productosAsignados = await _productoRepository.FindAsync(p => p.CategoriaId == id);
            if (productosAsignados.Any())
            {
                //Lanzamos una excepción que será capturada por el Middleware global
                throw new InvalidOperationException("No se puede eliminar la categoría porque tiene productos asignados. Por favor, cambie la categoría de los productos primero.");
            }

            var categoria = await _categoriaRepository.GetByIdAsync(id);
            if (categoria != null)
            {
                _categoriaRepository.Delete(categoria);
                await _categoriaRepository.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("La categoría que intenta eliminar no existe.");
            }
        }
    }
}