using AutoMapper;
using NexosTestBackend.Core.DTOs;
using NexosTestBackend.Data.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexosTestBackend.Core.Mappings
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            AllowNullCollections = true;

            CreateMap<Autor, AutorDto>().ReverseMap();
            CreateMap<Editorial, EditorialDto>()
                .ForMember(edi => edi.DireccionCorrespondencia, opt => opt.MapFrom(edidto => edidto.DirCorrespondencia))
                .ForMember(edi => edi.MaximoLibrosPermitidos, opt => opt.MapFrom(edidto => edidto.MaxLibrosReg))
                .ReverseMap();
            CreateMap<Libro, LibroDto>()
                .ForMember(lib => lib.NumeroPaginas, opt => opt.MapFrom(libro => libro.NumPaginas))                
                .ReverseMap();
        }
    }
}
