using ProgramAcad.Services.Modules.Usuarios.DTOs;
using System.Threading.Tasks;

namespace ProgramAcad.Services.Interfaces.Services
{
    public interface IAuthAdminAppService
    {
        Task<ListarUsuarioDTO> RegisterUserPasswordAsync(CadastrarUsuarioDTO usuario);
        Task<ListarUsuarioDTO> RegisterUserExternalAsync(CadastrarUsuarioDTO usuario);
        Task UpdateUserAsync(AtualizarUsuarioDTO usuario);
        Task ChangePasswordAsync(string email, string novaSenha);
        Task ChangeEmailAsync(string emailAntigo, string novoEmail);
        Task ConfirmEmailAsync(string email);
    }
}
