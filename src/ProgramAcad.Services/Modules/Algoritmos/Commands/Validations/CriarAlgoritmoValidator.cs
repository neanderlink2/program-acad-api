using FluentValidation;
using ProgramAcad.Common.Extensions;
using ProgramAcad.Common.Models;
using ProgramAcad.Domain.Contracts.Repositories;
using ProgramAcad.Domain.Entities;
using ProgramAcad.Services.Modules.Algoritmos.DTOs;
using ProgramAcad.Services.Modules.CasosTeste.Commands.Validations;
using System;
using System.Linq;

namespace ProgramAcad.Services.Modules.Algoritmos.Commands.Validations
{
    public class CriarAlgoritmoValidator : AbstractValidator<CriarAlgoritmoDTO>
    {
        private readonly INivelDificuldadeRepository _nivelDificuldadeRepository;

        public CriarAlgoritmoValidator(INivelDificuldadeRepository nivelDificuldadeRepository)
        {
            _nivelDificuldadeRepository = nivelDificuldadeRepository;

            ValidateTitulo();
            ValidateHtmlDescricao();
            ValidateNivelDificuldade();
            ValidateLinguagensPermitidas();
            ValidateCasosTeste();
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
                .Must(x => Enumeration.GetAll<LinguagemProgramacao>().Any(l => l.ApiIdentifier == x))
                    .WithMessage("Linguagem de programação inválida.");
        }

        public void ValidateCasosTeste()
        {
            RuleForEach(x => x.CasosTeste)
                .SetValidator(new CriarCasosTesteValidator());
        }
    }
}
