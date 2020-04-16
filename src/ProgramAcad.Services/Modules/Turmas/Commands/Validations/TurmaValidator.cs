using FluentValidation;
using ProgramAcad.Common.Extensions;
using ProgramAcad.Domain.Contracts.Repositories;
using ProgramAcad.Services.Modules.Turmas.DTOs;
using System;

namespace ProgramAcad.Services.Modules.Turmas.Commands.Validations
{
    public class TurmaValidator : AbstractValidator<BaseTurmaDTO>
    {
        private readonly ITurmaRepository _turmaRepository;

        public TurmaValidator(ITurmaRepository turmaRepository)
        {
            _turmaRepository = turmaRepository;

            ValidateDataTermino();
            ValidateNome();
            ValidateUrlImagem();
            ValidateCapacidadeAlunos();
        }

        public void ValidateNome()
        {
            RuleFor(x => x.NomeTurma)
                .NotEmpty().WithMessage("Nome da turma é obrigatório")
                .MinimumLength(3).WithMessage("O nome da turma deve possuir no mínimo 3 caracteres.")
                .MaximumLength(75).WithMessage("O nome da turma deve possuir no máximo 75 caracteres.")
                .MustAsync(async (nome, cancel) => !await _turmaRepository.AnyAsync(t => t.Nome.ToUpper() == nome.ToUpper()))
                    .WithMessage("Uma turma com este nome já está cadastrado.");
        }

        public void ValidateCapacidadeAlunos()
        {
            RuleFor(x => x.CapacidadeAlunos)
                .GreaterThan(5).WithMessage("A capacidade mínima de alunos deve ser maior que 5.")
                .LessThanOrEqualTo(2000).WithMessage("A capacidade máxima de alunos deve ser menor que 2000.");
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
