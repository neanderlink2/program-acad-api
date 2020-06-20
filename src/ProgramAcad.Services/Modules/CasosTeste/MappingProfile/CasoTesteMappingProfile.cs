using AutoMapper;
using ProgramAcad.Domain.Entities;
using ProgramAcad.Services.Modules.CasosTeste.DTOs;

namespace ProgramAcad.Services.Modules.CasosTeste.MappingProfile
{
    public class CasoTesteMappingProfile : Profile
    {
        public CasoTesteMappingProfile()
        {
            CreateMap<CasoTeste, CasoTesteDTO>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.EntradaEsperada, opt => opt.MapFrom(x => x.EntradaEsperada))
                .ForMember(x => x.SaidaEsperada, opt => opt.MapFrom(x => x.SaidaEsperada))
                .ForMember(x => x.TempoMaximoExecucao, opt => opt.MapFrom(x => x.TempoMaximoDeExecucao));
        }
    }
}
