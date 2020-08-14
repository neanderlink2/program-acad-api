using ProgramAcad.Common.Models.PagedList;

namespace ProgramAcad.Services.Modules.Turmas.DTOs
{
    public class PontuacaoTurmaDTO
    {
        public int MaximoPontos { get; set; }
        public IPagedList<UsuarioTurmaDTO> Inscritos { get; set; }
    }
}
