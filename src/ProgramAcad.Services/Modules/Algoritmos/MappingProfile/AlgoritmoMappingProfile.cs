using AutoMapper;
using ProgramAcad.Common.Extensions;
using ProgramAcad.Domain.Entities;
using ProgramAcad.Services.Modules.Algoritmos.DTOs;
using System.Linq;

namespace ProgramAcad.Services.Modules.Algoritmos.MappingProfile
{
    public class AlgoritmoMappingProfile : Profile
    {
        public AlgoritmoMappingProfile()
        {
            CreateMap<Algoritmo, ListarAlgoritmoDTO>()
                .ForMember(x => x.NivelDificuldade, opt => opt.MapFrom(x => x.NivelDificuldade.Nivel))
                .ForMember(x => x.LinguagensDisponiveis, opt => opt.MapFrom(x => x.LinguagensPermitidas.Select(x => x.IdLinguagem.GetDescription())))
                .ForMember(x => x.IdTurmaPertencente, opt => opt.MapFrom(x => x.IdTurma))
                .ForMember(x => x.NomeTurma, opt => opt.MapFrom(x => x.TurmaPertencente.Nome));
        }
    }
}
