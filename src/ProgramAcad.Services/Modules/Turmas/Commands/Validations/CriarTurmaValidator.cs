using FluentValidation;
using ProgramAcad.Common.Extensions;
using ProgramAcad.Services.Modules.Turmas.DTOs;
using System;

namespace ProgramAcad.Services.Modules.Turmas.Commands.Validations
{
    public class CriarTurmaValidator : AbstractValidator<CriarTurmaDTO>
    {
        public void ValidateNome()
        {
            RuleFor(x => x.NomeTurma)
                .NotEmpty().WithMessage("Nome da turma é obrigatório")
                .MinimumLength(3).WithMessage("O nome da turma deve possuir no mínimo 3 caracteres.")
                .MaximumLength(75).WithMessage("O nome da turma deve possuir no máximo 75 caracteres.");
        }

        public void ValidateDataTermino()
        {
            RuleFor(x => x.DataHoraTermino)
                .NotEmpty().WithMessage("A data de término é obrigatória.")
                .GreaterThanOrEqualTo(DateTime.Now.AddDays(7)).WithMessage("O curso deve possuir no mínimo uma semana de duração")
                .LessThanOrEqualTo(DateTime.Now.AddYears(4)).WithMessage("O curso não deve possuir uma duração maior do que 4 anos.");
        }

        public void ValidateUrlImagem()
        {
            RuleFor(x => x.UrlImagem)
                .Url().WithMessage("A URL da imagem está inválida.")
                .When(x => x.UrlImagem.HasValue());
        }
    }
}
