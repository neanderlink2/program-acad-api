using ProgramAcad.Domain.Entities;
using ProgramAcad.Domain.Models;
using ProgramAcad.Services.Modules.Algoritmos.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramAcad.Services.Interfaces.Services
{
    public interface IAlgoritmoAppService
    {
        Task<bool> CriarAlgoritmoAsync(CriarAlgoritmoDTO algoritmo);
        Task<bool> AtualizarAlgoritmoAsync(AtualizarAlgoritmoDTO algoritmo);
        Task<bool> DeletarAlgoritmoAsync(Guid idAlgoritmo);
        Task<IEnumerable<ListarAlgoritmoDTO>> ObterTodosAlgoritmosPorTurmaAsync(Guid idTurma);
        Task<ListarAlgoritmoDTO> ObterAlgoritmoPorIdAsync(Guid idAlgoritmo);
        Task<IEnumerable<ListarAlgoritmoDTO>> ObterAlgoritmosPorNivelDificuldadeAsync(Guid idTurma, int nivel);
        Task<IEnumerable<ListarAlgoritmoDTO>> ObterAlgoritmosPorLinguagemAsync(Guid idTurma, LinguagensProgramacao linguagensProgramacao);
        Task<IQueryable<KeyValueModel>> ObterNiveisDificuldadeDisponiveisAsync(Guid idTurma);
        Task<IQueryable<KeyValueModel>> ObterLinguagensDisponiveisAsync(Guid idTurma);
    }
}
