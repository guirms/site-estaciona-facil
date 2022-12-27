using Application.Objects.Requests.Usuario;
using Application.Objects.Responses.Usuario;

namespace Application.Interfaces;

public interface IUsuarioService
{
    UsuarioResponse CadastrarUsuario(UsuarioCadastroRequest usuarioCadastroRequest);
    UsuarioResponse RealizarLogin(UsuarioLoginRequest usuarioCadastroRequest);
}