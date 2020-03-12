using ProgramAcad.Common.Models.PagedList;
using ProgramAcad.Services.Modules.Turmas.DTOs;
using System.Threading.Tasks;

namespace ProgramAcad.Services.Interfaces.Services
{
    public interface ITurmaAppService
    {
        Task CriarTurma(CriarTurmaDTO criarTurma);
        Task EditarTurma(EditarTurmaDTO editarTurmaDTO);
        Task AceitarSolicitacaoAcesso(SolicitarAcessoTurmaDTO acesso);
        Task SolicitarAcesso(SolicitarAcessoTurmaDTO acesso);
        Task<IPagedList<ListarTurmaDTO>> GetTurmasPaged(string busca, int pageIndex, int totalItems, TurmaColunasOrdenacao colunaOrdenacao, string direcaoOrdenacao = "asc");
        Task<IPagedList<ListarTurmaDTO>> GetTurmasPagedByUsuario(string emailUsuario, string busca, int pageIndex, int totalItems, TurmaColunasOrdenacao colunaOrdenacao, string direcaoOrdenacao = "asc");
        Task<IPagedList<ListarTurmaDTO>> GetTurmasPagedByInstrutor(string emailInstrutor, string busca, int pageIndex, int totalItems, TurmaColunasOrdenacao colunaOrdenacao, string direcaoOrdenacao = "asc");
    }
}
