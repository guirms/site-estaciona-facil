namespace Infra.MessagePublisher.DTOs.MessageSenderDtos
{
    public record MessageSenderDataDto
    {
        public int QtdLinhas { get; init; } = default!;

        public MessageSenderDataDto(int qtdLinhas)
        {
            QtdLinhas = qtdLinhas;
        }
    }
}
