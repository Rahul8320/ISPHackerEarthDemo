using ISPHackerEarth.Application.Models.Responses;
using ISPHackerEarth.Domain.Entities;

namespace ISPHackerEarth.Application.Mapper;

public static class Mapping
{
    public static GetISPResponse ToModel(this ISP entity)
    {
        return new GetISPResponse()
        {
            Id = entity.Id,
            Name = entity.Name,
            Contact_No = entity.Contact_No,
            Description = entity.Description,
            Email = entity.Email,
            Image = entity.Image,
            Lowest_Price = entity.Lowest_Price,
            Max_Speed = entity.Max_Speed,
            Rating = entity.Rating,
            Url = entity.Url, 
            LastUpdated = entity.LastUpdated,
        };
    }
}
