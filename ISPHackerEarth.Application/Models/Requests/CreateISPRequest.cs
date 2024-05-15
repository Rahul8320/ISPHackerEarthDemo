using System.ComponentModel.DataAnnotations;

namespace ISPHackerEarth.Application.Models.Requests;

public class CreateISPRequest
{
    [Required, MinLength(2), MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [Required, Range(1, int.MaxValue)]
    public int Lowest_Price { get; set; }

    [Required, MinLength(2), MaxLength(15)]
    public string Max_Speed { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    [Required, MinLength(10), StringLength(10)]
    public string Contact_No { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required, Url]
    public string Image { get; set; } = string.Empty;

    [Required, Url]
    public string Url { get; set; } = string.Empty;
}
