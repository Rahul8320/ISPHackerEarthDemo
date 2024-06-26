﻿namespace ISPHackerEarth.Domain.Entities;

public class ISP : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public int Lowest_Price { get; set; }
    public double Rating { get; set; }
    public string Max_Speed { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Contact_No { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}
