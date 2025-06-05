namespace GameStore.Models.Configuration.CachePopulator
{
    public abstract class CacheConfiguration
    {
        public string Topic { get; set; } = string.Empty;

        public int RefreshInterval { get; set; } = 30;
    }
}
