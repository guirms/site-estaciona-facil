using Application.Interfaces;
using Application.Objects.Requests.Usuario;
using Application.Validators;
using AutoMapper;
using FluentValidation;
using Infra.Data.Interfaces;
using Moq;

namespace Tests.Setup;

public class TestsSetup
{
    # region Mock AutoMapper
    
    protected readonly IMapper AutoMapperMock;
    
    #endregion

    #region Mock Repositórios

    protected readonly Mock<IUsuarioRepository> UsuarioRepositoryMock;
    protected readonly Mock<IAutenticacaoService> AutenticacaoServiceMock;

    #endregion FluentValidation

    #region Validators

    protected readonly Mock<IValidator<UsuarioCadastroRequest>> UsuarioCadastroValidatorMock = new ();
    protected readonly Mock<IValidator<UsuarioLoginRequest>> UsuarioLoginValidatorMock = new ();
    protected readonly UsuarioValidator.UsuarioCadastroValidator UsuarioCadastroInstantiedValidator = new ();
    protected readonly UsuarioValidator.UsuarioLoginValidator UsuarioLoginInstantiedValidator = new ();


    #endregion
    
    public TestsSetup()
    {
        var autoMapperProfile = new Application.AutoMapper.AutoMapper();
        var configuration = new MapperConfiguration(x => x.AddProfile(autoMapperProfile));
        AutoMapperMock = new Mapper(configuration);
        
        UsuarioRepositoryMock = new Mock<IUsuarioRepository>();
        AutenticacaoServiceMock = new Mock<IAutenticacaoService>();
    }
}