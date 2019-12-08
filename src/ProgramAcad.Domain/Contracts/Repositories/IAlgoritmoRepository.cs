using ProgramAcad.Domain.Entities;
using ProgramAcad.Domain.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramAcad.Domain.Contracts.Repositories
{
    public interface IAlgoritmoRepository : IRepository<Algoritmo>
    {
        Task<IQueryable<KeyValueModel>> GetLingugagensProgramacaoFilterAsync(Guid idTurma);
        Task<IQueryable<KeyValueModel>> GetNiveisDificuldadeFilterAsync(Guid idTurma);
    }
}
