using RabbitMQ.Client;

namespace Report.Service.Services
{
    public class RabbitMQClientService:IDisposable
    {
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;
        public static string ExchangeName = "ReportDirectExchange";
        public static string RoutingReport = "report-route";
        public static string QueueName = "report-queue";

        public RabbitMQClientService(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public IModel Connect()
        {
            _connection = _connectionFactory.CreateConnection();

            if (_channel is { IsOpen: true })
            {
                return _channel;
            }

            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(ExchangeName, type:"direct", true, false);
            _channel.QueueDeclare(QueueName, true, false, false, null);
            _channel.QueueBind(QueueName,ExchangeName,RoutingReport);
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
