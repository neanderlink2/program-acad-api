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
        Task<IPagedList<ListarAlgoritmoDTO>> ObterTodosAlgoritmosPorTurmaAsync(Guid idTurma, string emailUsuario, string busca, int numPagina, int qtdePorPagina,
            ColunasOrdenacaoAlgoritmo colunasOrdenacao, string direcaoOrdenacao);
        Task<ListarAlgoritmoDTO> ObterAlgoritmoPorIdAsync(Guid idAlgoritmo);
        Task<IPagedList<ListarAlgoritmoDTO>> ObterAlgoritmosPorNivelDificuldadeAsync(int nivel, Guid idTurma, string emailUsuario, string busca, int numPagina, int qtdePorPagina,
            ColunasOrdenacaoAlgoritmo colunasOrdenacao, string direcaoOrdenacao);
        Task<IPagedList<ListarAlgoritmoDTO>> ObterAlgoritmosPorLinguagemAsync(int linguagem, Guid idTurma, string emailUsuario, string busca, int numPagina, int qtdePorPagina,
            ColunasOrdenacaoAlgoritmo colunasOrdenacao, string direcaoOrdenacao);
        Task<IQueryable<KeyValueModel>> ObterNiveisDificuldadeDisponiveisAsync(Guid idTurma);
        Task<IQueryable<KeyValueModel>> ObterLinguagensDisponiveisAsync(Guid idTurma);
    }
}
