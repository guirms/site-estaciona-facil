using Application.Objects.Requests.Usuario;
using Application.Services;
using Domain.Models;
using Moq;
using Tests.Setup;
using Xunit;

namespace Tests.Services;

public class UsuarioServiceTest: TestsSetup
{
    private UsuarioService _usuarioService;
    public UsuarioServiceTest()
    {
        _usuarioService = new UsuarioService(AutoMapperMock, 
            UsuarioRepositoryMock.Object,
            AutenticacaoServiceMock.Object);
    }
    
    [Fact]
    public void CadastrarUsuario_EnviandoEmailInvalido_DeveRetornarEmailInvalido()
    {
        var usuarioTeste = new UsuarioCadastroRequest("emailinvalido", "teste123", "teste123");
        
        var chamadaMetodo = Assert.Throws<Exception>(() => _usuarioService.CadastrarUsuario(usuarioTeste));
        
        Assert.Equal("Email em formato inválido", chamadaMetodo.Message);
    }
    
     [Fact]
     public void CadastrarUsuario_EnviandoEmailNulo_DeveRetornarEmailInvalido()
     {
         var usuarioTeste = new UsuarioCadastroRequest("", "teste", "teste");

         var chamadaMetodo = Assert.Throws<Exception>(() => _usuarioService.CadastrarUsuario(usuarioTeste));
        
         Assert.Equal("Email em formato inválido", chamadaMetodo.Message);
     }
     
     [Fact]
     public void CadastrarUsuario_EnviandoSenhaNula_DeveRetornarSenhaNula()
     {
         var usuarioTeste = new UsuarioCadastroRequest("testeunitario@teste.com", "", "");
         
         var chamadaMetodo = Assert.Throws<NullReferenceException>(() => _usuarioService.CadastrarUsuario(usuarioTeste));
        
         Assert.Equal("Senha nula é inválida", chamadaMetodo.Message);
     }
     
     [Fact]
     public void CadastrarUsuario_EnviandoUsuarioJaExistente_DeveRetornarUsuarioExistente()
     {
         var usuarioTeste = new UsuarioCadastroRequest("teste@teste.com", "teste", "teste");

         AutenticacaoServiceMock.Setup(a => a.GerarSenhaHashMd5(usuarioTeste.Senha)).Returns("teste");
         UsuarioRepositoryMock.Setup(u => u.ConsultarUsuarioIdPorEmailESenha(usuarioTeste.Email, usuarioTeste.Senha)).Returns(10);
         
         _usuarioService = new UsuarioService(AutoMapperMock, UsuarioRepositoryMock.Object, AutenticacaoServiceMock.Object);
         
         var chamadaMetodo = Assert.Throws<Exception>(() => _usuarioService.CadastrarUsuario(usuarioTeste));
        
         Assert.Equal("Usuário já cadastrado no sistema", chamadaMetodo.Message);
     }

     [Fact]
     public void CadastrarUsuario_GerandoTokenNulo_ErroAoGerarToken()
     {
         var usuarioTeste = new UsuarioCadastroRequest("teste@teste.com", "teste", "teste");

         UsuarioRepositoryMock.Setup(u => u.SalvarUsuario(It.IsAny<Usuario>())).Returns(10);
         AutenticacaoServiceMock.Setup(a => a.GerarSenhaHashMd5(usuarioTeste.Senha)).Returns("teste");
         UsuarioRepositoryMock.Setup(u => u.ConsultarUsuarioIdPorEmailESenha(usuarioTeste.Email, "teste")).Returns(0);
         AutenticacaoServiceMock.Setup(a => a.GerarTokenSessao(usuarioTeste.Email, usuarioTeste.Senha))
             .Returns(string.Empty);

         var chamadaMetodo = Assert.Throws<Exception>(() => _usuarioService.CadastrarUsuario(usuarioTeste));
         
         Assert.Equal("Erro ao gerar token de sessão", chamadaMetodo.Message);
     }


     [Fact]
     public void RealizarLogin_EnviandoEmailInvalido_DeveRetornarEmailInvalido()
     {          
         var usuarioTeste = new UsuarioLoginRequest("emailinvalido", "teste123");

         var chamadaMetodo = Assert.Throws<Exception>(() => _usuarioService.RealizarLogin(usuarioTeste));
        
         Assert.Equal("Email em formato inválido", chamadaMetodo.Message);
     }
     
     [Fact]
     public void RealizarLogin_EnviandoEmailNulo_DeveRetornarEmailInvalido()
     {
         var usuarioTeste = new UsuarioLoginRequest("", "teste123");

         var chamadaMetodo = Assert.Throws<Exception>(() => _usuarioService.RealizarLogin(usuarioTeste));
        
         Assert.Equal("Email em formato inválido", chamadaMetodo.Message);
     }
     
    
     [Fact]
     public void RealizarLogin_EnviandoLoginInvalido_EmailOuSenhaInvalidos()
     {
         var usuarioTeste = new UsuarioLoginRequest("teste@teste.com", "teste123");

         UsuarioRepositoryMock.Setup(u => u.ConsultarUsuarioIdPorEmailESenha(usuarioTeste.Email, usuarioTeste.Senha)).Returns(0);
         
         var chamadaMetodo = Assert. Throws<NullReferenceException>(() => _usuarioService.RealizarLogin(usuarioTeste));
         
         Assert.Equal("Usuário ou senha inválidos", chamadaMetodo.Message);
     }
     
     [Fact]
     public void RealizarLogin_GerandoTokenNulo_ErroAoGerarToken()
     {
         var usuarioTeste = new UsuarioLoginRequest("teste@teste.com", "teste123");
         
         AutenticacaoServiceMock.Setup(a => a.GerarSenhaHashMd5(usuarioTeste.Senha)).Returns("teste");
         UsuarioRepositoryMock.Setup(u => u.ConsultarUsuarioIdPorEmailESenha(usuarioTeste.Email, "teste")).Returns(10);
         AutenticacaoServiceMock.Setup(u => u.GerarTokenSessao(usuarioTeste.Email, It.IsAny<string>())).Returns(string.Empty);

         var chamadaMetodo = Assert. Throws<Exception>(() => _usuarioService.RealizarLogin(usuarioTeste));
         
         Assert.Equal("Erro ao gerar token de sessão", chamadaMetodo.Message);
     }
}
