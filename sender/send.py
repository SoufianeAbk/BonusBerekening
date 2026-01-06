import pika

url = "amqps://student:XYR4yqc.cxh4zug6vje@rabbitmq-exam.rmq3.cloudamqp.com/mxifnklj"
exchange = "exchange.23a0537b-c891-47eb-9cb3-32e12b524afb"
routing_key = "23a0537b-c891-47eb-9cb3-32e12b524afb"
queue = "exam"

params = pika.URLParameters(url)
# Use BlockingConnection which supports AMQPS URIs
connection = pika.BlockingConnection(params)
channel = connection.channel()

# Declare durable direct exchange
channel.exchange_declare(exchange=exchange, exchange_type='direct', durable=True)

# Bind the existing queue to our exchange
channel.queue_bind(queue=queue, exchange=exchange, routing_key=routing_key)

# Publish persistent message
channel.basic_publish(exchange=exchange,
                      routing_key=routing_key,
                      body="Hi CloudAMQP, this was fun!",
                      properties=pika.BasicProperties(delivery_mode=2))
print('Message published')

# Clean up: unbind and delete the exchange
channel.queue_unbind(queue=queue, exchange=exchange, routing_key=routing_key)
channel.exchange_delete(exchange=exchange)

connection.close()
print('Cleanup done and connection closed')
