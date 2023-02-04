using Infra.MessagePublisher.DTOs.MessageSenderDtos;

namespace Infra.MessagePublisher.Interfaces
{
    public interface IMessageSender
    {
        void Publicar(MessageSenderDataDto data);
    }
}
