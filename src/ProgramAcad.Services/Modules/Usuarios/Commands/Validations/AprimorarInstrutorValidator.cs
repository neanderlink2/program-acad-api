using FluentValidation;
using ProgramAcad.Domain.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramAcad.Services.Modules.Usuarios.Commands.Validations
{
    public class AprimorarInstrutorValidator : AbstractValidator<Guid>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public AprimorarInstrutorValidator(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public void ValidateIdUsuario()
        {
            RuleFor(idUsuario => idUsuario)
                .NotEmpty().WithMessage("Um identificador do usuário é necessário.")
                .MustAsync(async (idUsuario, cancel) => await _usuarioRepository.AnyAsync(u => u.Id == idUsuario));
        }
    }
}
