namespace Application.Objects.Requests.Autenticacao;

public class AutenticacaoTokenRequest
{
    public AutenticacaoTokenRequest(string email, string senha)
    {
        Email = email;
        Senha = senha;
    }

    public string Email { get; set; }
    public string Senha { get; set; }
}