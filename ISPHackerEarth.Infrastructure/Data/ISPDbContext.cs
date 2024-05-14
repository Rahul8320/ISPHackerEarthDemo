using ISPHackerEarth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ISPHackerEarth.Infrastructure.Data;

public class ISPDbContext(DbContextOptions<ISPDbContext> options) : DbContext(options)
{
    public DbSet<ISP>ISPs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ISP>().ToTable("ISPs");

        SeedDataToDb(modelBuilder);
    }

    private static void SeedDataToDb(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ISP>().HasData(
            new ISP()
            {
                Id = Guid.NewGuid(),
                Name = "BSNL",
                Lowest_Price = 399,
                Description = "Bharat Sanchar Nigam Limited | Explore our latest offers and hot tariffs of various services and get activated instantly online. BSNL offers Telecom Training for Students and Professionals. BSNL offers Spaces/Land available for Rent/sale. Mobile Explore various Promotional Offers for Mobile.",
                Contact_No = "1234567890",
                Max_Speed = "30 Mbps",
                Email = "info@bsnl.com",
                Rating = 4.0,
                Image = "https://www.bsnl.co.in/Logo-New.png",
                Url = "https://www.bsnl.co.in/",
            },
            new ISP()
            {
                Id = Guid.NewGuid(),
                Name = "Airtel",
                Lowest_Price = 499,
                Description = "Bharti Airtel Limited, commonly known as Airtel, is an Indian multinational telecommunications services company based in New Delhi. It operates in 18 countries across South Asia and Africa, as well as the Channel Islands. Currently, Airtel provides 5G, 4G and LTE Advanced services throughout India. Currently offered services include fixed-line broadband, and voice services depending upon the country of operation.",
                Contact_No = "2134789045",
                Max_Speed = "Upto 40 Mbps",
                Email = "info@airtel.com",
                Rating = 4.4,
                Image = "https://assets.airtel.in/static-assets/new-home/img/airtel-red.svg",
                Url = "https://www.airtel.in/",
            },
            new ISP()
            {
                Id = Guid.NewGuid(),
                Name = "Jio",
                Lowest_Price = 599,
                Description = "Jio is an Indian telecommunications company and a subsidiary of Jio Platforms, headquartered in Navi Mumbai, Maharashtra. It operates a national LTE network with coverage across all 22 telecom circles. Jio offers 5G, 4G and 4G+ services all over India and 5G service almost All Over India. Its 6G service is in the works. Jio soft launched on 27 December 2015 with a beta for partners and employees, and became publicly available on 5 September 2016.",
                Contact_No = "6789012345",
                Max_Speed = "Upto 30 Mbps",
                Email = "info@airtel.com",
                Rating = 4.2,
                Image = "https://jep-asset.akamaized.net/cms/assets/jiofiber/discover/excited-to-experience-jiofiber.png",
                Url = "https://www.jio.com/fiber",
            });
    }
}
