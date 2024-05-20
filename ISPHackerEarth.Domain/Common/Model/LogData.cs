namespace ISPHackerEarth.Domain.Common.Model;

public class LogData
{
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string Message { get; set; } = string.Empty;
    public Guid IspId { get; set; }
    public string IspName { get; set; } = string.Empty;
}
