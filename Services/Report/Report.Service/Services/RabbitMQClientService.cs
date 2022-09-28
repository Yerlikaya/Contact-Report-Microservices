using RabbitMQ.Client;

namespace Report.Service.Services
{
    public class RabbitMQClientService:IDisposable
    {
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;

        public RabbitMQClientService(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public IModel Connect(string queue, string routing, string exchange)
        {
            _connection = _connectionFactory.CreateConnection();

            if (_channel is { IsOpen: true })
            {
                return _channel;
            }

            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange, type:"direct", true, false);
            _channel.QueueDeclare(queue, true, false, false, null);
            _channel.QueueBind(queue, exchange, routing);
            return _channel;
        }

        public void Dispose()
        {
            _channel?.Close();
            _channel.Dispose();

            _connection.Close();
            _connection?.Dispose();
        }
    }
}
