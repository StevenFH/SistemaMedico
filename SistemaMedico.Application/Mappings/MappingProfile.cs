using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using SistemaMedico.Domain.Entities;
using SistemaMedico.Application.DTOs;

namespace SistemaMedico.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Mapeo Categoria <-> CategoriaDTO
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();

            //Mapeo Producto <-> ProductoDTO (incluyendo el nombre de la categoría)
            CreateMap<Producto, ProductoDTO>()
                .ForMember(dest => dest.CategoriaNombre, opt => opt.MapFrom(src => src.Categoria.Nombre));

            CreateMap<ProductoDTO, Producto>();
        }
    }
}