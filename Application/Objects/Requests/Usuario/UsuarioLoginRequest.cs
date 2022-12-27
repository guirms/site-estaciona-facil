namespace Application.Objects.Requests.Usuario;

public class UsuarioLoginRequest
{
    public UsuarioLoginRequest(string email, string senha)
    {
        Email = email;
        Senha = senha;
    }

    public string Email { get; set; }
    public string Senha { get; set; }
}