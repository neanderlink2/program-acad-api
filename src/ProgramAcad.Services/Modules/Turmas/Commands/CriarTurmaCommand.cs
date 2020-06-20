using ProgramAcad.Common.Notifications;
using ProgramAcad.Domain.Contracts.Repositories;
using ProgramAcad.Domain.Entities;
using ProgramAcad.Domain.Workers;
using ProgramAcad.Services.Modules.Common;
using ProgramAcad.Services.Modules.Turmas.Commands.Validations;
using ProgramAcad.Services.Modules.Turmas.DTOs;
using System.Threading.Tasks;

namespace ProgramAcad.Services.Modules.Turmas.Commands
{
    public class CriarTurmaCommand : Command<CriarTurmaDTO>
    {
        private readonly TurmaValidator _validation;
        private readonly ITurmaRepository _turmaRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public CriarTurmaCommand(TurmaValidator validation, ITurmaRepository turmaRepository, IUsuarioRepository usuarioRepository,
            DomainNotificationManager notifyManager, IUnitOfWork unitOfWork)
            : base(notifyManager, unitOfWork)
        {
            _validation = validation;
            _turmaRepository = turmaRepository;
            _usuarioRepository = usuarioRepository;
        }

        public override async Task<bool> ExecuteAsync(CriarTurmaDTO turma)
        {
            _validation.ValidateNomeExistente();
            var result = _validation.Validate(turma);
            await NotifyValidationErrorsAsync(result);
            if (_notifyManager.HasNotifications()) return false;

            var instrutor = await _usuarioRepository.GetSingleAsync(x => x.Email.ToUpper() == turma.EmailInstrutor.ToUpper() && x.IsAtivo);

            var turmaEntity = new Turma(instrutor.Id, turma.NomeTurma, turma.CapacidadeAlunos, turma.UrlImagem, turma.DataCriacao, turma.DataHoraTermino);
            await _turmaRepository.AddAsync(turmaEntity);

            return await CommitChangesAsync();
        }
    }
}
