using ProgramAcad.Domain.Contracts.Repositories;
using ProgramAcad.Domain.Entities;
using ProgramAcad.Domain.Models;
using ProgramAcad.Infra.Data.Workers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramAcad.Infra.Data.Repository.Contracts
{
    public class AlgoritmoRepository : Repository<Algoritmo>, IAlgoritmoRepository
    {
        public AlgoritmoRepository(ProgramAcadDataContext dbContext) : base(dbContext)
        {
        }

        public Task<IQueryable<KeyValueModel>> GetLingugagensProgramacaoFilterAsync(Guid idTurma)
        {
            var query = $@"SELECT l.id AS 'Key', l.nome AS 'Value' FROM TB_ALGORITMO a
	                          INNER JOIN TB_ALGORITMO_LINGUAGEM_DISPONIVEL al ON a.id = al.id_algoritmo
	                          INNER JOIN TB_LINGUAGEM_PROGRAMACAO l ON l.id = al.id_linguagem_programacao
	                          WHERE a.bl_ativo = 1 AND a.id_turma = '{idTurma}'";
            return Task.FromResult(FromSql<KeyValueModel>(query));
        }

        public Task<IQueryable<KeyValueModel>> GetNiveisDificuldadeFilterAsync(Guid idTurma)
        {
            var query = $@"SELECT n.id AS 'Key', n.descricao AS 'Value' FROM TB_ALGORITMO a
	                          INNER JOIN TB_NIVEL_DIFICULDADE n ON a.id_nivel_dificuldade = n.id
	                          WHERE a.bl_ativo = 1 a.id_turma = '{idTurma}'";
            return Task.FromResult(FromSql<KeyValueModel>(query));
        }
    }
}
