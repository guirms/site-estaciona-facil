using Application.Objects.Requests.Usuario;
using Application.Objects.Responses.Usuario;
using AutoMapper;
using Domain.Models;

namespace Application.AutoMapper;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        UsuarioMap();
    }

    #region Usuario

    private void UsuarioMap()
    {
        CreateMap<UsuarioCadastroRequest, Usuario>();
        
        CreateMap<UsuarioLoginRequest, UsuarioResponse>();
        
        CreateMap<Usuario, UsuarioResponse>();
    }

    #endregion
    
}
    