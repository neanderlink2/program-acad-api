using ProgramAcad.Common.Models;
using ProgramAcad.Common.Notifications;
using ProgramAcad.Domain.Contracts.Repositories;
using ProgramAcad.Domain.Entities;
using ProgramAcad.Domain.Workers;
using ProgramAcad.Services.Modules.Algoritmos.Commands.Validations;
using ProgramAcad.Services.Modules.Algoritmos.DTOs;
using ProgramAcad.Services.Modules.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramAcad.Services.Modules.Algoritmos.Commands
{
    public class CriarAlgoritmoCommand : Command<CriarAlgoritmoDTO>
    {
        private readonly CriarAlgoritmoValidator _validation;
        private readonly IAlgoritmoRepository _algoritmoRepository;
        private readonly INivelDificuldadeRepository _nivelDificuldadeRepository;

        public CriarAlgoritmoCommand(CriarAlgoritmoValidator validation,
            IAlgoritmoRepository algoritmoRepository, INivelDificuldadeRepository nivelDificuldadeRepository, DomainNotificationManager notifyManager, IUnitOfWork unitOfWork) : base(notifyManager, unitOfWork)
        {
            _validation = validation;
            _algoritmoRepository = algoritmoRepository;
            _nivelDificuldadeRepository = nivelDificuldadeRepository;
        }

        public override async Task<bool> ExecuteAsync(CriarAlgoritmoDTO algoritmo)
        {
            var validacoes = _validation.Validate(algoritmo);
            await NotifyValidationErrorsAsync(validacoes);

            if (_notifyManager.HasNotifications()) return false;

            var nivelDificuldade = await _nivelDificuldadeRepository.GetSingleAsync(x => x.Nivel == algoritmo.NivelDificuldade);

            var entity = new Algoritmo(algoritmo.IdTurma, algoritmo.Titulo, algoritmo.HtmlDescricao, nivelDificuldade.Id, algoritmo.DataCriacao);
            entity.SetLinguagensProgramacao(algoritmo.LinguagensPermitidas);

            foreach (var casoTeste in algoritmo.CasosTeste)
            {
                entity.CasosDeTeste.Add(new CasoTeste(casoTeste.EntradaEsperada, casoTeste.SaidaEsperada,
                    casoTeste.TempoMaximoExecucao, entity.Id));
            }

            await _algoritmoRepository.AddAsync(entity);

            return await CommitChangesAsync();
        }

        private IEnumerable<AlgoritmoLinguagemDisponivel> ObterLinguagensFromString(IEnumerable<string> linguagens, Guid idAlgoritmo)
        {
            //transforma o Enum em IEnumerable
            var enumLinguagens = Enumeration.GetAll<LinguagemProgramacao>();
            if (enumLinguagens.Any(x => linguagens.Contains(x.ApiIdentifier)))
            {
                foreach (var item in linguagens)
                {
                    var linguagem = enumLinguagens.FirstOrDefault(x => x.ApiIdentifier.Equals(item));
                    //Para cada linguagem solicitada, Busca o primeiro valor que está contido dentro do array de linguagens.
                    if (linguagem != default)
                    {
                        yield return new AlgoritmoLinguagemDisponivel(idAlgoritmo, linguagem.Id);
                    }
                }
            }
            //Se não possuir nenhuma linguagem, retornar Enumerable vazio.
            yield break;
        }
    }
}
