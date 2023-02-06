using Application.Objects.Bases;
using Infra.MessagePublisher.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController, AllowAnonymous]
[Route("Vaga")]
public class VagaController : ControllerBase
{
    private readonly IRabbitMqConfig _rabbitMqConfig;

    public VagaController(IRabbitMqConfig rabbitMqConfig)
    {
        _rabbitMqConfig = rabbitMqConfig;
    }

    [HttpPost("GerarRelatorio")]
    public JsonResult GerarRelatorio(int qtdLinhas)
    {
        try
        {
            if (qtdLinhas == 0)
                throw new InvalidOperationException("Não é possível gerar um relatório sem linhas");

            _rabbitMqConfig.Publicar(new MessageSenderRequest(qtdLinhas));

            return ResponseBase.ResponderController(true, "Relatório gerado com sucesso");
        }
        catch (Exception e)
        {
            return ResponseBase.ResponderController(false, e.Message);
        }
    }
}