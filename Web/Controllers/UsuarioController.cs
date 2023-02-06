using Application.Interfaces;
using Application.Objects.Bases;
using Application.Objects.Requests.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController, AllowAnonymous]
[Route("Usuario")]
public class UsuarioController: ControllerBase
{
    private readonly IUsuarioService _usuarioService;
    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }
    
    [HttpPost("RealizarLogin")]
    public JsonResult RealizarLogin([FromBody] UsuarioLoginRequest usuarioLoginRequest)
    {
        try
        {
            if (usuarioLoginRequest == null)
                throw new NullReferenceException("Usuário nulo");
            
            var cadastrarUsuario = _usuarioService.RealizarLogin(usuarioLoginRequest);
        
            return ResponseBase.ResponderController(true, "Login efetuado com sucesso", cadastrarUsuario);
        }
        catch (Exception e)
        {
            return ResponseBase.ResponderController(false, e.Message);
        }
    }
    
    [HttpPost("CadastrarUsuario")]
    public JsonResult CadastrarUsuario([FromBody] UsuarioCadastroRequest usuarioCadastroRequest)
    {
        try
        {
            if (usuarioCadastroRequest == null)
                throw new NullReferenceException("Usuário nulo");
            
            var cadastrarUsuario = _usuarioService.CadastrarUsuario(usuarioCadastroRequest);
        
            return ResponseBase.ResponderController(true, "Usuário cadastrado com sucesso", cadastrarUsuario);
        }
        catch (Exception e)
        {
            return ResponseBase.ResponderController(false, e.Message);
        }
    }
}