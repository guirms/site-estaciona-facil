namespace Domain.Models;

public class Usuario
{
    public Usuario(string email, string senha)
    {
        Email = email;
        Senha = senha;
    }

    public int UsuarioId { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
}