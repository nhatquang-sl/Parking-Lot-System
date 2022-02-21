namespace PLS.Application.Common.Logging
{
    public class LogEntry
    {
        public LogLevel LogLevel { get; set; }
        public string MessageTemplate { get; set; }
        public object[] PropertyValues { get; set; }
    }
}
