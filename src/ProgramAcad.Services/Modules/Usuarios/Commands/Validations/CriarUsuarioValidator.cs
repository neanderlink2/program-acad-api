using FluentValidation;
using FluentValidation.Results;
using ProgramAcad.Domain.Contracts.Repositories;
using ProgramAcad.Services.Modules.Usuarios.DTOs;

namespace ProgramAcad.Services.Modules.Usuarios.Commands.Validations
{
    public class CriarUsuarioValidator : AbstractValidator<CadastrarUsuarioDTO>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public CriarUsuarioValidator(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public ValidationResult ValidateForExternal(CadastrarUsuarioDTO usuario)
        {
            ValidateNickname();
            return Validate(usuario);
        }

        public ValidationResult ValidateForPassword(CadastrarUsuarioDTO usuario)
        {
            ValidateNome();
            ValidateEmail();
            ValidateSenha();
            ValidateNickname();
            return Validate(usuario);
        }

        private void ValidateNome()
        {
            RuleFor(x => x.NomeCompleto)
                .NotNull().WithMessage("Nome completo é obrigatório.")
                .MinimumLength(3).WithMessage("O nome deve conter no mínimo 3 caracteres.");
        }

        private void ValidateEmail()
        {
            RuleFor(x => x.Email)
                .NotNull().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("O e-mail digitado é inválido.")
                .MinimumLength(3).WithMessage("O e-mail deve conter no mínimo 3 caracteres.");
        }

        private void ValidateSenha()
        {
            RuleFor(x => x.Senha)
                .NotNull().WithMessage("A senha é obrigatória.")
                .Matches("[A-Z]").WithMessage("A senha deve conter uma letra maiúscula.")
                .Matches("[a-z]").WithMessage("A senha deve conter uma letra minúscula.")
                .Matches("[0-9]").WithMessage("A senha deve conter um número.")
                .Matches("[!@#$%&*)(]").WithMessage("A senha deve conter um caracter especial.")
                .MinimumLength(8).WithMessage("A senha deve possuir no mínimo 8 caracteres.");
        }

        private void ValidateNickname()
        {
            RuleFor(x => x.Nickname)
                .NotNull().WithMessage("O nickname é obrigatório.")
                .MustAsync(async (nickname, cancel) => !await _usuarioRepository.AnyAsync(u => u.Nickname == nickname))
                    .WithMessage("O nickname já está sendo utilizado.");
        }
    }
}
