using Infra.MessagePublisher.DTOs.MessageSenderDtos;
using Infra.MessagePublisher.Interfaces;
using RabbitMQ.Client;
using System.Text;

namespace Infra.MessagePublisher.RabbitMQ
{
    public sealed class RabbitMqConfig : IMessageSender
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _queueName;


        public RabbitMqConfig()
        {
            _queueName = "relatorio-queue";

            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "user",
                Password = "password",
                Port = 5672
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void Publicar(MessageSenderDataDto data)
        {
            _channel.ExchangeDeclare(
                exchange: "ms-relatorio-ex",
                type: "direct",
                durable: true,
                autoDelete: false,
                arguments: null
                );

            _channel.QueueDeclare(
                queue: _queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );

            _channel.QueueBind(_queueName, "ms-relatorio-ex", "gerar-relatorio");

            var body = Encoding.UTF8.GetBytes(data.QtdLinhas.ToString());

            _channel.BasicPublish(
                exchange: "ms-relatorio-ex", 
                routingKey: "gerar-relatorio",
                basicProperties: null,
                body: body
                );
        }

        public void Dispose()
        {
            _connection.Dispose();
            _channel.Dispose();
        }
    }
}
