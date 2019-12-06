using FluentValidation;
using ProgramAcad.Common.Extensions;
using ProgramAcad.Domain.Contracts.Repositories;
using ProgramAcad.Domain.Entities;
using ProgramAcad.Services.Modules.Algoritmos.DTOs;
using ProgramAcad.Services.Modules.CasosTeste.Commands.Validations;
using System;
using System.Linq;

namespace ProgramAcad.Services.Modules.Algoritmos.Commands.Validations
{
    public class AtualizarAlgoritmoValidator : AbstractValidator<AtualizarAlgoritmoDTO>
    {
        private readonly IAlgoritmoRepository _algoritmoRepository;
        private readonly INivelDificuldadeRepository _nivelDificuldadeRepository;

        public AtualizarAlgoritmoValidator(IAlgoritmoRepository algoritmoRepository, INivelDificuldadeRepository nivelDificuldadeRepository)
        {
            _algoritmoRepository = algoritmoRepository;
            _nivelDificuldadeRepository = nivelDificuldadeRepository;
        }

        public void ValidateId()
        {
            RuleFor(x => x.Id)
                .NotNull()
                    .WithMessage("ID do algoritmo é obrigatório.")
                .NotEmpty()
                    .WithMessage("ID do algoritmo é obrigatório.")
                .MustAsync(async (id, cancel) => await _algoritmoRepository.AnyAsync(a => a.Id == id))
                    .WithMessage("Algoritmo não foi encontrado.");
        }

        public void ValidateTitulo()
        {
            RuleFor(x => x.Titulo)
                .NotNull()
                    .WithMessage("O título do algoritmo é obrigatório.")
                .MinimumLength(3)
                    .WithMessage("O título deve conter mais de 3 caracteres.");
        }

        public void ValidateHtmlDescricao()
        {
            RuleFor(x => x.HtmlDescricao)
                .NotNull()
                    .WithMessage("A descrição do algoritmo é obrigatória.")
                .MinimumLength(20)
                    .WithMessage("A descrição deve possuir no mínimo 20 caracteres.");
        }

        public void ValidateNivelDificuldade()
        {
            RuleFor(x => x.NivelDificuldade)
                .GreaterThan(0)
                    .WithMessage("O nível de dificuldade não pode ser menor ou igual a zero.")
                .MustAsync(async (nivel, cancel) => await _nivelDificuldadeRepository.AnyAsync(n => n.Nivel == nivel))
                    .WithMessage("Nenhum nível de dificuldade foi encontrado com este ID");
        }

        public void ValidateLinguagensPermitidas()
        {
            RuleForEach(x => x.LinguagensPermitidas)
                .Must(x => Enum.GetValues(typeof(LinguagensProgramacao)).Cast<LinguagensProgramacao>().Any(l => l.GetDescription() == x))
                    .WithMessage("Linguagem de programação inválida.");
        }

        public void ValidateCasosTeste()
        {
            RuleForEach(x => x.CasosTeste)
                .SetValidator(new CriarCasosTesteValidator());
        }
    }
}
