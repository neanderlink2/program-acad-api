using FluentValidation;
using ProgramAcad.Domain.Contracts.Repositories;
using ProgramAcad.Services.Modules.Turmas.DTOs;

namespace ProgramAcad.Services.Modules.Turmas.Commands.Validations
{
    public class AceitarSolicitacaoAcessoValidator : AbstractValidator<SolicitarAcessoTurmaDTO>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITurmaRepository _turmaRepository;

        public AceitarSolicitacaoAcessoValidator(IUsuarioRepository usuarioRepository, ITurmaRepository turmaRepository)
        {
            _usuarioRepository = usuarioRepository;
            _turmaRepository = turmaRepository;
        }

        public void ValidateEmailUsuario()
        {
            RuleFor(x => x.EmailUsuario)
                .NotEmpty().WithMessage("O e-mail é obrigatório")
                .MustAsync(async (email, cancel) => await _usuarioRepository.AnyAsync(x => x.Email.ToUpper().Equals(email.ToUpper())))
                    .WithMessage(x => $"O e-mail {x.EmailUsuario} não está cadastrado.");
        }

        public void ValidateIdTurma()
        {
            RuleFor(x => x.IdTurma)
                .NotEmpty().WithMessage("O identificador da turma é obrigatório")
                .MustAsync(async (idTurma, cancel) => await _turmaRepository.AnyAsync(x => x.Id.Equals(idTurma)))
                    .WithMessage(x => $"O e-mail {x.EmailUsuario} não está cadastrado.");
        }
    }
}
