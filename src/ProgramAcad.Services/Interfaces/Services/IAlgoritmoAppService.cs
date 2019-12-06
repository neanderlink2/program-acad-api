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
        Task<bool> CriarAlgoritmo(CriarAlgoritmoDTO algoritmo);
        Task<bool> AtualizarAlgoritmo(AtualizarAlgoritmoDTO algoritmo);
        Task<bool> DeletarAlgoritmo(Guid idAlgoritmo);
        Task<ICollection<ListarAlgoritmoDTO>> ObterTodosAlgoritmosPorTurma(Guid idTurma);
        Task<ListarAlgoritmoDTO> ObterAlgoritmoPorId(Guid idAlgoritmo);
        Task<ICollection<ListarAlgoritmoDTO>> ObterAlgoritmosPorNivelDificuldade(Guid idTurma, int nivel);
        Task<ICollection<ListarAlgoritmoDTO>> ObterAlgoritmosPorLinguagem(Guid idTurma, LinguagensProgramacao linguagensProgramacao);
        Task<IQueryable<KeyValueModel>> ObterNiveisDificuldadeDisponiveis(Guid idTurma);
        Task<IQueryable<KeyValueModel>> ObterLinguagensDisponiveis(Guid idTurma);
    }
}
