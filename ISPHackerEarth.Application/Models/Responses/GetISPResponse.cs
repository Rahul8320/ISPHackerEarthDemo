namespace ISPHackerEarth.Application.Models.Responses;

public class GetISPResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Lowest_Price { get; set; }
    public double Rating { get; set; }
    public string Max_Speed { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Contact_No { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
}
