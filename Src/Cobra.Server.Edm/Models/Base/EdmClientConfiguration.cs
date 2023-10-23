namespace Cobra.Server.Edm.Models.Base
{
    public class EdmClientConfiguration
    {
        public int MetricsThreshold { get; set; }
        public int MetricsPriorityThreshold { get; set; }
        public bool UsageTrackingEnabled { get; set; }
        public int UsageTrackingSamplingInterval { get; set; }
        public int UsageTrackingMetricsInterval { get; set; }
    }
}
