using FluentValidation;
using ProgramAcad.Common.Extensions;
using ProgramAcad.Domain.Contracts.Repositories;
using ProgramAcad.Services.Modules.Usuarios.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramAcad.Services.Modules.Usuarios.Commands.Validations
{
    public class AtualizarUsuarioValidator : AbstractValidator<AtualizarUsuarioDTO>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public AtualizarUsuarioValidator(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;

            ValidateNome();
            ValidateCep();
            ValidateCpf();
            ValidateDataNascimento();
            ValidateSexo();
        }

        private void ValidateNome()
        {
            RuleFor(x => x.NomeCompleto)
                .NotNull().WithMessage("Nome completo é obrigatório.")
                .MinimumLength(3).WithMessage("O nome deve conter no mínimo 3 caracteres.");
        }

        private void ValidateSexo()
        {
            RuleFor(x => x.Sexo)
                .Sexo();
        }

        private void ValidateCep()
        {
            RuleFor(x => x.Cep)
                .Cep();
        }

        private void ValidateCpf()
        {
            RuleFor(x => x.Cpf)
                .Cpf()
                .MustAsync(async (cpf, cancel) => !await _usuarioRepository.AnyAsync(x => x.Cpf == cpf))
                    .WithMessage(x => $"O CPF '{x.Cpf}' já está cadastrado no sistema. Por favor, utilize outro.");
        }

        private void ValidateDataNascimento()
        {
            RuleFor(x => x.DataNascimento)
                .LessThan(DateTime.Now).WithMessage("Você nasceu hoje? 🤔")
                .GreaterThan(DateTime.Parse("1900-01-01"));
        }
    }
}