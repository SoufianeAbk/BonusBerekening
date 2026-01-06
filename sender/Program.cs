using System;
using System.Text;
using RabbitMQ.Client;

namespace RabbitSender
{
    internal class Program
    {
        static int Main()
        {
            var uri = new Uri("amqps://student:XYR4yqc.cxh4zug6vje@rabbitmq-exam.rmq3.cloudamqp.com/mxifnklj");
            var routingKey = "23a0537b-c891-47eb-9cb3-32e12b524afb";
            var exchangeName = "exchange." + routingKey;
            var queueName = "exam";

            try
            {
                var factory = new ConnectionFactory() { Uri = uri };
                // Newer RabbitMQ.Client versions expose async connection creation. Use the async API and wait synchronously here.
                using var conn = factory.CreateConnectionAsync().GetAwaiter().GetResult();
                // Use the async model creation if synchronous API is not present in this client version.
                using var channel = conn.CreateModelAsync().GetAwaiter().GetResult();

                // Declare a durable direct exchange (we must not use predeclared exchanges)
                channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, durable: true, autoDelete: false);

                // Bind the existing queue 'exam' to our exchange with the required routing key
                channel.QueueBind(queueName, exchangeName, routingKey);

                var body = Encoding.UTF8.GetBytes("Hi CloudAMQP, this was fun!");
                var props = channel.CreateBasicProperties();
                props.DeliveryMode = 2; // persistent

                channel.BasicPublish(exchangeName, routingKey, props, body);
                Console.WriteLine("Message published.");

                // Clean up: unbind and delete exchange we created
                channel.QueueUnbind(queueName, exchangeName, routingKey, null);
                channel.ExchangeDelete(exchangeName);

                Console.WriteLine("Cleanup done. Closing connection.");
                return 0;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex}");
                return 1;
            }
        }
    }
}
