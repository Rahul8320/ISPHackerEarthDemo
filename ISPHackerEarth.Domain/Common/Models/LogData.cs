namespace ISPHackerEarth.Domain.Common.Models;

public class LogData(string message, Guid? ispId = null, string? ispName = null)
{
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string Message { get; set; } = message;
    public Guid? IspId { get; set; } = ispId;
    public string? IspName { get; set; } = ispName;
}
