using ProgramAcad.Domain.Entities;
using ProgramAcad.Domain.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramAcad.Domain.Contracts.Repositories
{
    public interface IAlgoritmoRepository : IRepository<Algoritmo>
    {
        Task<IQueryable<KeyValueModel>> GetLingugagensProgramacaoFilter(Guid idTurma);
        Task<IQueryable<KeyValueModel>> GetNiveisDificuldadeFilter(Guid idTurma);
    }
}
