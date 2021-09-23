namespace Local.Kafka.Workshop.Producer
{
    using Confluent.Kafka;
    using emp.maersk.com;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Nucleus.Kafka.Abstractions;
    using Nucleus.Kafka.Common;
    using Nucleus.Kafka.Producing;
    using Nucleus.Kafka.Producing.Configuration;
    using Nucleus.Kafka.SchemaRegistry.Abstractions.Configuration;
    using Nucleus.Kafka.SchemaRegistry.Avro;
    using System;
    using System.Collections.Generic;

    internal class Program
    {
        private static readonly Random _random = new Random();

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

            var producerBuilder = new ProducerBuilder(logHandler, serializer);

            var producerFactoryLogger = LoggerFactory
                            .Create(logger => logger.AddConsole())
                            .CreateLogger<ProducerFactory>();

            var producerFactory = new ProducerFactory(producerFactoryLogger, producerBuilder);

            var configuration = new KafkaLocalProducerConfiguration(
                "myproducer",
                new[] { "localhost:9092" },
                schemaRegistryConfiguration.Value);

            using (var producer = producerFactory.Create<KeySchema, datedSchedule>(configuration))
            {
                var producerLogger = LoggerFactory
                                .Create(logger => logger.AddConsole())
                                .CreateLogger<IProducer<KeySchema, datedSchedule>>();

                Action<DeliveryReport<KeySchema, datedSchedule>> deliveryHandler = r =>
                {
                    if (r.Error.IsError)
                    {
                        producerLogger.LogError("Failed to produce message: {errorReason}", r.Error.Reason);
                    }
                    else
                    {
                        producerLogger.LogDebug("Message with {key} was delivered to {topicPartitionOffset}", r.Key, r.TopicPartitionOffset);
                    }
                };

                var messages = GenerateMessages(10000);

                foreach (var message in messages)
                {
                    producer.Produce("MSK.schedule.datedSchedule.topic.public.any.v5", message, deliveryHandler);
                }

                producer.Flush();
            }
        }

        private static IEnumerable<Message<KeySchema, datedSchedule>> GenerateMessages(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var key = _random.Next(10000000, 20000000).ToString();

                var message = new Message<KeySchema, datedSchedule>
                {
                    Key = new KeySchema
                    {
                        messageKey = key
                    },
                    Value = new datedSchedule
                    {
                        scheduleEntries = new scheduleEntries
                        {
                            scheduleEntry = new List<scheduleEntry_record>
                            {
                                {
                                    new scheduleEntry_record
                                    {
                                        actual = new actual
                                        {
                                            actualArrival = "",
                                            actualDeparture = "",
                                            arrivalAtPilotStation = "",
                                            firstPilotOnBoard = "",
                                            pilotOff = ""
                                        },
                                        arrivalStatus = "SCHEDULED",
                                        departureStatus = "SCHEDULED",
                                        dummyCall = "",
                                        notes = "",
                                        omitReason = "",
                                        pH1 = "",
                                        pH2 = "",
                                        rotationId = "XYZ",
                                        rotationName = "",
                                        rotationVersion = "1",
                                        schedule = new schedule
                                        {
                                            proformaArrival = "",
                                            proformaDeparture = "",
                                            scheduledArrival = "",
                                            scheduledDeparture = ""
                                        },
                                        scheduleEntryID = new scheduleEntryID
                                        {
                                            scheduleEntryKey = key,
                                            scheduleEntryIdentifier = new scheduleEntryIdentifier
                                            {
                                                arrivalService = new arrivalService
                                                {
                                                    code = "",
                                                    name = "",
                                                    transportMode = "FEF"
                                                },
                                                arrivalVoyage = new arrivalVoyage
                                                {
                                                    direction = "NW",
                                                    voyage = ""
                                                },
                                                currentPortCall = new currentPortCall
                                                {
                                                    cityCode = "",
                                                    cityGeoCode = "",
                                                    cityName = "",
                                                    terminalCode = "",
                                                    terminalGeoCode = "",
                                                    terminalName = ""
                                                },
                                                departureService = new departureService
                                                {
                                                    code = "",
                                                    name = "",
                                                    transportMode = "FEF"
                                                },
                                                departureVoyage = new departureVoyage
                                                {
                                                    direction = "NW",
                                                    voyage = ""
                                                },
                                                nextPortCall = new nextPortCall
                                                {
                                                    cityCode = "",
                                                    cityGeoCode = "",
                                                    cityName = "",
                                                    terminalCode = "",
                                                    terminalGeoCode = "",
                                                    terminalName = "",
                                                    nextScheduleEntryKey = "",
                                                    arrivalVoyage = "",
                                                    departureVoyage = ""
                                                },
                                                previousPortCall = new previousPortCall
                                                {
                                                    cityCode = "",
                                                    cityGeoCode = "",
                                                    cityName = "",
                                                    terminalCode = "",
                                                    terminalGeoCode = "",
                                                    terminalName = "",
                                                    previousScheduleEntryKey = "",
                                                    arrivalVoyage = "",
                                                    departureVoyage = ""
                                                },
                                                vessel = new vessel
                                                {
                                                    IMONumber = "",
                                                    vesselCallSign = "",
                                                    vesselCode = "",
                                                    vesselFlag = "",
                                                    vesselName = "",
                                                    vesselOperatorCode = ""
                                                }
                                            }
                                        },
                                        siteCallStatus = "",
                                        updatedBy = ""
                                    }
                                }
                            }
                        }
                    }
                };

                yield return message;
            }
        }
    }
}
