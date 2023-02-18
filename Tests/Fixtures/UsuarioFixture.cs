using Application.Interfaces;
using Application.Objects.Requests.Usuario;
using Application.Services;
using Application.Validators;
using FluentValidation;
using Infra.Data.Interfaces;
using Moq;
using Tests.Setup;
using Xunit;

namespace Tests.Collections;

[CollectionDefinition(nameof(UsuarioFixture))]
public class UsuarioFixture : TestsSetup
{
    protected UsuarioService _usuarioService;
    protected readonly Mock<IUsuarioRepository> UsuarioRepositoryMock;
    protected readonly Mock<IAutenticacaoService> AutenticacaoServiceMock;
    protected readonly Mock<IValidator<UsuarioCadastroRequest>> UsuarioCadastroValidatorMock = new();
    protected readonly Mock<IValidator<UsuarioLoginRequest>> UsuarioLoginValidatorMock = new();
    protected readonly UsuarioValidator.UsuarioCadastroValidator UsuarioCadastroInstantiedValidator = new();
    protected readonly UsuarioValidator.UsuarioLoginValidator UsuarioLoginInstantiedValidator = new();

    public UsuarioFixture()
    {
        UsuarioRepositoryMock = CriarInstancia<IUsuarioRepository>();
        AutenticacaoServiceMock = CriarInstancia<IAutenticacaoService>();

        _usuarioService = new UsuarioService(AutoMapperMock,
            UsuarioRepositoryMock.Object,
            AutenticacaoServiceMock.Object,
            UsuarioCadastroValidatorMock.Object,
            UsuarioLoginValidatorMock.Object);
    }

}