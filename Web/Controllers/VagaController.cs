using Application.Objects.Bases;
using Application.Objects.Requests.Usuario;
using Infra.MessagePublisher.DTOs.MessageSenderDtos;
using Infra.MessagePublisher.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController, AllowAnonymous]
[Route("Vaga")]
public class VagaController : ControllerBase
{
    private readonly IMessageSender _messageSender;

    public VagaController(IMessageSender messageSender)
    {
        _messageSender = messageSender;
    }

    [HttpPost("GerarRelatorio")]
    public JsonResult GerarRelatorio(int qtdLinhas)
    {
        try
        {
            if (qtdLinhas == 0)
                throw new InvalidOperationException("Não é possível gerar um relatório sem linhas");

            _messageSender.Publicar(new MessageSenderDataDto(qtdLinhas));

            return ResponseBase.ResponderController(true, "Relatório gerado com sucesso");
        }
        catch (Exception e)
        {
            return ResponseBase.ResponderController(false, e.Message);
        }
    }
}