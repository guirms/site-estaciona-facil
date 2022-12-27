namespace Application.Objects.Requests.Usuario;

public class UsuarioCadastroRequest
{
    public UsuarioCadastroRequest(string email, string senha, string confirmacaoSenha)
    {
        Email = email;
        Senha = senha;
        ConfirmacaoSenha = confirmacaoSenha;
    }

    public string Email { get; set; }
    public string Senha { get; set; }
    public string ConfirmacaoSenha { get; set; }
}