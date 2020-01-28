using ProgramAcad.Common.Constants;
using ProgramAcad.Common.Notifications;
using ProgramAcad.Domain.Contracts.Repositories;
using ProgramAcad.Domain.Entities;
using ProgramAcad.Domain.Workers;
using ProgramAcad.Services.Modules.CasosTeste.DTOs;
using ProgramAcad.Services.Modules.Common;
using System.Threading.Tasks;

namespace ProgramAcad.Services.Modules.CasosTeste.Commands
{
    public class SalvarAlgoritmoConcluidoCommand : Command<SalvarAlgoritmoConcluidoDTO>
    {
        private readonly IAlgoritmoResolvidoRepository _algoritmoResolvidoRepository;
        private readonly ITurmaUsuarioRepository _turmaUsuarioRepository;
        private readonly IAlgoritmoRepository _algoritmoRepository;

        public SalvarAlgoritmoConcluidoCommand(IAlgoritmoResolvidoRepository algoritmoResolvidoRepository,
            ITurmaUsuarioRepository turmaUsuarioRepository, IAlgoritmoRepository algoritmoRepository,
            DomainNotificationManager notifyManager, IUnitOfWork unitOfWork) : base(notifyManager, unitOfWork)
        {
            _algoritmoResolvidoRepository = algoritmoResolvidoRepository;
            _turmaUsuarioRepository = turmaUsuarioRepository;
            _algoritmoRepository = algoritmoRepository;
        }

        public override async Task<bool> ExecuteAsync(SalvarAlgoritmoConcluidoDTO algoritmoConcluido)
        {
            var algoritmo = await _algoritmoRepository.GetSingleAsync(x => x.Id == algoritmoConcluido.IdAlgoritmo, "NivelDificuldade");
            if (algoritmo == null)
            {
                await NotifyAsync(NotifyReasons.NOT_FOUND, "Algoritmo não encontrado");
                return false;
            }

            var turma = await _turmaUsuarioRepository.GetSingleAsync(x => x.IdUsuario == algoritmoConcluido.IdUsuario && algoritmo.IdTurma == x.IdTurma);
            if (turma == null)
            {
                await NotifyAsync(NotifyReasons.FORBIDDEN, "Usuário não pertence a essa turma.");
                return false;
            }

            var algoritmoEntity = new AlgoritmoResolvido(algoritmoConcluido.IdUsuario, algoritmoConcluido.IdAlgoritmo, algoritmoConcluido.LinguagemUtilizada,
                algoritmoConcluido.DataConclusao);

            await _algoritmoResolvidoRepository.AddAsync(algoritmoEntity);
            turma.AddPontos(algoritmo.NivelDificuldade.PontosReceber);
            await _turmaUsuarioRepository.UpdateAsync(turma);

            return await CommitChangesAsync();
        }
    }
}
