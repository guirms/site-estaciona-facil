using Application.Interfaces;
using Application.Objects.Bases;
using Application.Objects.Requests.Autenticacao;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("Token")]
public class AutenticacaoController: ControllerBase
{
    private readonly IAutenticacaoService _autenticacaoService;
    public AutenticacaoController(IAutenticacaoService autenticacaoService)
    {
        _autenticacaoService = autenticacaoService;
    }
    
    [HttpPost("GerarTokenSessao")]
    public JsonResult GerarTokenSessao(AutenticacaoTokenRequest autenticacaoTokenRequest)
    {
        try
        {
            var token = _autenticacaoService.GerarTokenSessao(autenticacaoTokenRequest.Email, _autenticacaoService.GerarSenhaHashMd5(autenticacaoTokenRequest.Senha));
         
            return ResponseBase.ResponderController(true, "Token gerado com sucesso", token);
        }
        catch (Exception e)
        {
            return ResponseBase.ResponderController(false, $"Erro ao gerar token: {e.Message}");
        }
    }
}