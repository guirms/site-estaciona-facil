using Application.Interfaces;
using Application.Objects.Requests.Usuario;
using Application.Services;
using Application.Validators;
using FluentValidation;
using Infra.Data.Interfaces;
using Moq;
using Tests.Setup;
using Xunit;

namespace Tests.Collections
{
    [CollectionDefinition(nameof(UsuarioCollection))]
    public class UsuarioCollection : TestsSetup
    {
        protected UsuarioService _usuarioService;

        public UsuarioCollection()
        {
            _usuarioService = new UsuarioService(AutoMapperMock,
                UsuarioRepositoryMock.Object,
                AutenticacaoServiceMock.Object,
                UsuarioCadastroValidatorMock.Object,
                UsuarioLoginValidatorMock.Object);
        }

        #region Mock Repository

        protected readonly Mock<IUsuarioRepository> UsuarioRepositoryMock = CriarInstancia<IUsuarioRepository>();
        protected readonly Mock<IAutenticacaoService> AutenticacaoServiceMock = CriarInstancia<IAutenticacaoService>();

        #endregion

        #region Validators

        protected readonly Mock<IValidator<UsuarioCadastroRequest>> UsuarioCadastroValidatorMock = new();
        protected readonly Mock<IValidator<UsuarioLoginRequest>> UsuarioLoginValidatorMock = new();
        protected readonly UsuarioValidator.UsuarioCadastroValidator UsuarioCadastroInstantiedValidator = new();
        protected readonly UsuarioValidator.UsuarioLoginValidator UsuarioLoginInstantiedValidator = new();

        #endregion
    }
}
