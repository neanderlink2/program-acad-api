using AutoMapper;
using ProgramAcad.Domain.Entities;
using ProgramAcad.Services.Modules.Usuarios.DTOs;

namespace ProgramAcad.Services.Modules.Usuarios.MappingProfile
{
    public class UsuarioMappingProfile : Profile
    {
        public UsuarioMappingProfile()
        {
            CreateMap<Usuario, ListarUsuarioDTO>()
                .ConstructUsing(x => new ListarUsuarioDTO
                {
                    Id = x.Id,
                    NomeCompleto = x.NomeCompleto,
                    Email = x.Email,
                    Nickname = x.Nickname,
                    Cep = x.Cep,
                    Cpf = x.Cpf,
                    DataNascimento = x.DataNascimento,
                    Sexo = x.Sexo
                });
        }
    }
}
