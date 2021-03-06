﻿using ProgramAcad.Common.Constants;
using ProgramAcad.Common.Notifications;
using ProgramAcad.Domain.Contracts.Repositories;
using ProgramAcad.Domain.Workers;
using ProgramAcad.Services.Modules.Common;
using System;
using System.Threading.Tasks;

namespace ProgramAcad.Services.Modules.Algoritmos.Commands
{
    public class DeletarAlgoritmoCommand : Command<Guid>
    {
        private readonly IAlgoritmoRepository _algoritmoRepository;

        public DeletarAlgoritmoCommand(IAlgoritmoRepository algoritmoRepository,
           DomainNotificationManager notifyManager, IUnitOfWork unitOfWork) : base(notifyManager, unitOfWork)
        {
            _algoritmoRepository = algoritmoRepository;
        }

        public override async Task<bool> ExecuteAsync(Guid idAlgoritmo)
        {
            var algoritmo = await _algoritmoRepository.GetSingleAsync(x => x.Id == idAlgoritmo);

            if (algoritmo == null)
            {
                await NotifyAsync(NotifyReasons.NOT_FOUND, "Algoritmo não foi encontrado.");
                return false;
            }
            algoritmo.Deactivate();
            await _algoritmoRepository.UpdateAsync(algoritmo);
            return await CommitChangesAsync();
        }
    }
}
