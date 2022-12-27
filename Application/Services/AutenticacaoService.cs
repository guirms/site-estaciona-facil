using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Interfaces;
using Infra.Data.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class AutenticacaoService: IAutenticacaoService
{
    private readonly IConfiguration _configuration;
    private readonly IUsuarioRepository _usuarioRepository;

    public AutenticacaoService(IConfiguration configuration, IUsuarioRepository usuarioRepository)
    {
        _configuration = configuration;
        _usuarioRepository = usuarioRepository;
    }
    
    public string GerarTokenSessao(string emailUsuario, string senhaUsuarioHash)
    {
        var usuarioId = _usuarioRepository.ConsultarUsuarioIdPorEmailESenha(emailUsuario, senhaUsuarioHash) ?? throw new InvalidOperationException("Usuário inexistente");

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["SecretKey"] ?? String.Empty);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Email, emailUsuario),
                new Claim(ClaimTypes.NameIdentifier, usuarioId.ToString()),
            }),
            Expires = DateTime.UtcNow.AddHours(3),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    
    public string GerarSenhaHashMd5(string senha)
    {
        MD5 md5Hash = MD5.Create();

        byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(senha));

        StringBuilder sBuilder = new StringBuilder();

        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }

        return sBuilder.ToString();
    }
}