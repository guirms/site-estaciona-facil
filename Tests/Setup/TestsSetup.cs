using Application.Interfaces;
using AutoMapper;
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