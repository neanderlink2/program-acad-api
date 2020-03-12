using Bogus;
using ProgramAcad.Services.Modules.Turmas.DTOs;
using System;

namespace ProgramAcad.UnitTests.Fakers
{
    public static class TurmaFaker
    {
        public static Faker<CriarTurmaDTO> CreateCriarTurmaDTO(Guid idInstrutor)
        {
            var faker = new Faker<CriarTurmaDTO>()
                .RuleFor(x => x.CapacidadeAlunos, setter => setter.Random.Int(0, 500))
                .RuleFor(x => x.NomeTurma, setter => setter.Company.CompanyName())
                .RuleFor(x => x.IdInstrutor, setter => idInstrutor)
                .RuleFor(x => x.UrlImagem, setter => setter.Internet.Avatar())
                .RuleFor(x => x.DataCriacao, setter => DateTime.Now)
                .RuleFor(x => x.DataHoraTermino, (setter, turma) => setter.Date.Future(2, turma.DataCriacao));

            return faker;
        }
    }
}