namespace Local.Kafka.Workshop.Consumer
{
    using Confluent.Kafka;
    using emp.maersk.com;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Nucleus.Kafka.Abstractions;
    using Nucleus.Kafka.Common;
    using Nucleus.Kafka.Consuming;
    using Nucleus.Kafka.Consuming.Configuration;
    using Nucleus.Kafka.SchemaRegistry.Abstractions.Configuration;
    using Nucleus.Kafka.SchemaRegistry.Avro;
    using System.Threading;

    internal class Program
    {
        static void Main(string[] args)
        {
            var schemaRegistryConfiguration = Options.Create(new SchemaRegistryConfiguration
            {
                Url = "http://localhost:8081"
            });

            var schemaRegistryClientFactory = new SchemaRegistryClientFactory();
            var serializer = new AvroSerializerFactory(schemaRegistryConfiguration, schemaRegistryClientFactory);

            var logHandlerLogger = LoggerFactory
                            .Create(logger => logger.AddConsole())
                            .CreateLogger<LogHandler>();
            var logHandler = new LogHandler(logHandlerLogger);

            var consumerFactoryLogger = LoggerFactory
                            .Create(logger => logger.AddConsole())
                            .CreateLogger<ConsumerClientFactory>();

            var consumerFactory = new ConsumerClientFactory(
                consumerFactoryLogger,
                serializer,
                logHandler,
                null);

            var topic = "MSK.schedule.datedSchedule.topic.public.any.v5";

            var configuration = new KafkaConsumerConfiguration
            {
                Topic = topic,
                BootstrapServers = new[] { "localhost:9092" },
                Name = "DatedScheduleConsumer",
                ConsumerGroupId = "dscconsumer-group",
                StatisticsIntervalMs = 0,
                EnableAutoOffsetStore = false,
                EnableAutoCommit = true,
                SchemaRegistry = new SchemaRegistryConfiguration
                {
                    Url = "http://localhost:8081"
                }
            };

            var cts = new CancellationTokenSource();

            using (var consumer = consumerFactory.Create<KeySchema, datedSchedule>(configuration))
            {
                var consumerLogger = LoggerFactory
                                .Create(logger => logger.AddConsole())
                                .CreateLogger<IConsumer<KeySchema, datedSchedule>>();

                consumer.Subscribe(topic);

                ConsumeResult<KeySchema, datedSchedule>? consumeResult = null;

                while (!cts.IsCancellationRequested)
                {
                    consumeResult = consumer.Consume(5000);

                    if (consumeResult?.Message?.Key is not null)
                    {
                        consumerLogger.LogInformation("Consumed message with {key}", consumeResult.Message.Key.messageKey);
                    }
                }

                consumerLogger.LogInformation("Consumer has stopped.");
            }
        }
    }
}
