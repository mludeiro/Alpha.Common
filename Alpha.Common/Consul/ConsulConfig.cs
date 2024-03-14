namespace Alpha.Common.Consul;

public class ConsulConfig
{
    public string? ConsulAddress { get; set; }
    public string? ServiceId { get; set; }

    public int? IntervalSeconds  { get; set; }

    public int? TimeoutSeconds  { get; set; }

    public int? DeregisterCriticalServiceAfterSeconds  { get; set; }

}
