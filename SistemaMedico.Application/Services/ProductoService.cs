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
    public class ProductoService : IProductoService
    {
        private readonly IGenericRepository<Producto> _productoRepository;
        private readonly IMapper _mapper;

        public ProductoService(IGenericRepository<Producto> productoRepository, IMapper mapper)
        {
            _productoRepository = productoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductoDTO>> GetAllAsync()
        {
            var productos = await _productoRepository.GetAllAsync(p => p.Categoria);
            return _mapper.Map<IEnumerable<ProductoDTO>>(productos);
        }

        public async Task<ProductoDTO?> GetByIdAsync(int id)
        {
            var producto = await _productoRepository.GetByIdAsync(id);
            return producto != null ? _mapper.Map<ProductoDTO>(producto) : null;
        }

        public async Task CreateAsync(ProductoDTO productoDto)
        {
            var producto = _mapper.Map<Producto>(productoDto);
            await _productoRepository.AddAsync(producto);
            await _productoRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductoDTO productoDto)
        {
            var producto = _mapper.Map<Producto>(productoDto);
            _productoRepository.Update(producto);
            await _productoRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var producto = await _productoRepository.GetByIdAsync(id);
            if (producto != null)
            {
                _productoRepository.Delete(producto);
                await _productoRepository.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("El producto que intenta eliminar no existe.");
            }
        }
    }
}