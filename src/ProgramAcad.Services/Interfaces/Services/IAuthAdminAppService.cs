using ProgramAcad.Services.Modules.Usuarios.DTOs;
using System.Threading.Tasks;

namespace ProgramAcad.Services.Interfaces.Services
{
    public interface IAuthAdminAppService
    {
        Task<ListarUsuarioDTO> RegisterUserPasswordAsync(CadastrarUsuarioDTO usuario);
        Task<ListarUsuarioDTO> RegisterUserExternalAsync(CadastrarUsuarioDTO usuario);
        Task UpdateUserAsync(string email, AtualizarUsuarioDTO usuario);
        Task ChangePasswordAsync(TrocaSenhaDTO trocaSenha);
        Task ChangeEmailAsync(string emailAntigo, string novoEmail);
        Task ConfirmEmailAsync(string email);
    }
}
