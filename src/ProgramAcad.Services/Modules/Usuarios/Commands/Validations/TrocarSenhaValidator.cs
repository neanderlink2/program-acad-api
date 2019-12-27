using FluentValidation;
using ProgramAcad.Common.Extensions;
using ProgramAcad.Domain.Contracts.Repositories;
using ProgramAcad.Services.Modules.Usuarios.DTOs;

namespace ProgramAcad.Services.Modules.Usuarios.Commands.Validations
{
    public class TrocarSenhaValidator : AbstractValidator<TrocaSenhaDTO>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public TrocarSenhaValidator(IUsuarioRepository usuarioRepository)
        {
            ValidateEmail();
            ValidateSenhaNova();
            _usuarioRepository = usuarioRepository;
        }

        private void ValidateSenhaNova()
        {
            RuleFor(x => x.SenhaNova)
                .Senha()
                .Must((objeto, x) => x != objeto.SenhaAntiga).WithMessage("A nova senha deve ser diferente da senha atual.");
        }

        private void ValidateEmail()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage(x => $"O e-mail '{x.Email}' é inválido.")
                .MustAsync(async (email, cancel) => await _usuarioRepository.AnyAsync(x => x.Email.ToUpper() == email.ToUpper()))
                    .WithMessage("O e-mail digitado não está cadastrado no sistema.");
        }
    }
}
