namespace GameStore.Models.Configuration.KafkaCache
{
    public abstract class BaseKafkaCacheConfig
    {
        public string BootstrapServer { get; set; } = string.Empty;

        public string Topic { get; set; } = string.Empty;
    }
}
