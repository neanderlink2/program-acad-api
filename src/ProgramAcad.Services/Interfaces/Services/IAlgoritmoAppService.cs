using ProgramAcad.Common.Models.PagedList;
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
        Task<ListarAlgoritmoDTO> CriarAlgoritmoAsync(CriarAlgoritmoDTO algoritmo);
        Task<ListarAlgoritmoDTO> AtualizarAlgoritmoAsync(AtualizarAlgoritmoDTO algoritmo);
        Task<bool> DeletarAlgoritmoAsync(Guid idAlgoritmo);
        Task<IPagedList<ListarAlgoritmoDTO>> ObterTodosAlgoritmosPorTurmaAsync(Guid idTurma, int numPagina, int qtdePorPagina);
        Task<ListarAlgoritmoDTO> ObterAlgoritmoPorIdAsync(Guid idAlgoritmo);
        Task<IPagedList<ListarAlgoritmoDTO>> ObterAlgoritmosPorNivelDificuldadeAsync(Guid idTurma, int nivel, int numPagina, int qtdePorPagina);
        Task<IPagedList<ListarAlgoritmoDTO>> ObterAlgoritmosPorLinguagemAsync(Guid idTurma, LinguagensProgramacao linguagensProgramacao, int numPagina, int qtdePorPagina);
        Task<IQueryable<KeyValueModel>> ObterNiveisDificuldadeDisponiveisAsync(Guid idTurma);
        Task<IQueryable<KeyValueModel>> ObterLinguagensDisponiveisAsync(Guid idTurma);
    }
}
