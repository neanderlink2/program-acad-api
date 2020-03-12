﻿using ProgramAcad.Common.Notifications;
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

        public CriarTurmaCommand(TurmaValidator validation, ITurmaRepository turmaRepository,
            DomainNotificationManager notifyManager, IUnitOfWork unitOfWork) 
            : base(notifyManager, unitOfWork)
        {
            _validation = validation;
            _turmaRepository = turmaRepository;
        }

        public override async Task<bool> ExecuteAsync(CriarTurmaDTO turma)
        {
            var result = _validation.Validate(turma);
            await NotifyValidationErrorsAsync(result);
            if (_notifyManager.HasNotifications()) return false;

            var turmaEntity = new Turma(turma.IdInstrutor, turma.NomeTurma, turma.CapacidadeAlunos, turma.UrlImagem, turma.DataCriacao, turma.DataHoraTermino);
            await _turmaRepository.AddAsync(turmaEntity);

            return await CommitChangesAsync();
        }
    }
}
