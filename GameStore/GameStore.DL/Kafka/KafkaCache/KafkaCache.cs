using Confluent.Kafka;
using GameStore.Models.Serialization;
using Microsoft.Extensions.Hosting;

namespace GameStore.DL.Kafka.KafkaCache
{
    public class KafkaCache<TKey, TValue> : BackgroundService
    {
        private readonly ConsumerConfig _config;

        public KafkaCache()
        {
            _config = new ConsumerConfig
            {
                BootstrapServers = "192.168.1.16:9093",
                GroupId = $"KafkaChat-{Guid.NewGuid()}",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                SecurityProtocol = SecurityProtocol.SaslPlaintext,
                SaslMechanism = SaslMechanism.Plain,
                SaslUsername = "admin",
                SaslPassword = "CPxpKSRD"
            };
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Task.Run(() => ConsumeMessages(stoppingToken), stoppingToken);

            return Task.CompletedTask;
        }

        private void ConsumeMessages(CancellationToken stoppingToken)
        {
            using (var consumer = new ConsumerBuilder<TKey, TValue>(_config)
              .SetValueDeserializer(new MessagePackDeserializer<TValue>())
              .Build())
            {
                consumer.Subscribe("movies_cache");

                while (!stoppingToken.IsCancellationRequested)
                {
                    var consumeResult = consumer.Consume();

                    if (consumeResult.IsPartitionEOF)
                    {
                        continue;
                    }

                    if (consumeResult != null)
                    {
                        Console.WriteLine($"Error: {consumeResult.Message.Key}");
                    }
                }
            }
        }
    }
}
