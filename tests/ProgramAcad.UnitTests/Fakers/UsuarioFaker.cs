using Bogus;
using Bogus.Extensions.Brazil;
using ProgramAcad.Domain.Entities;
using ProgramAcad.Services.Modules.Usuarios.DTOs;

namespace ProgramAcad.UnitTests.Fakers
{
    public static class UsuarioFaker
    {
        public static Faker<Usuario> CreateEntity()
        {
            var faker = new Faker<Usuario>("pt_BR")
                .RuleFor(x => x.NomeCompleto, setter => setter.Name.FullName())
                .RuleFor(x => x.Nickname, (setter, user) => setter.Internet.UserName(user.NomeCompleto.Split(" ")[0], user.NomeCompleto.Split(" ")[1]))
                .RuleFor(x => x.Email, (setter, user) => setter.Internet.Email(user.NomeCompleto.Split(" ")[0], user.NomeCompleto.Split(" ")[1]))
                .RuleFor(x => x.Cpf, setter => setter.Person.Cpf())
                .RuleFor(x => x.Cep, setter => setter.Random.ReplaceNumbers("##.###-###"))
                .RuleFor(x => x.Sexo, setter => setter.PickRandom("M", "F"))
                .RuleFor(x => x.Role, setter => setter.PickRandom("INSTRUTOR", "ESTUDANTE"))
                .RuleFor(x => x.IsUsuarioExterno, setter => setter.Random.Bool());

            return faker;
        }

        public static Faker<CadastrarUsuarioDTO> CreateDTO()
        {
            var faker = new Faker<CadastrarUsuarioDTO>("pt_BR")
                .RuleFor(x => x.NomeCompleto, setter => setter.Name.FullName())
                .RuleFor(x => x.Nickname, (setter, user) => setter.Internet.UserName(user.NomeCompleto.Split(" ")[0], user.NomeCompleto.Split(" ")[1]))
                .RuleFor(x => x.Email, (setter, user) => setter.Internet.Email(user.NomeCompleto.Split(" ")[0], user.NomeCompleto.Split(" ")[1]))
                .RuleFor(x => x.Senha, (setter, user) => setter.Internet.Password(12));

            return faker;
        }
    }
}
