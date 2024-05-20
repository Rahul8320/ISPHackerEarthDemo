using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ISPHackerEarth.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBaseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ISPs",
                keyColumn: "Id",
                keyValue: new Guid("7007b758-e569-44b1-b6d1-e89ff731132f"));

            migrationBuilder.DeleteData(
                table: "ISPs",
                keyColumn: "Id",
                keyValue: new Guid("a6b16136-5acf-4d26-9d3e-50686d3f55c7"));

            migrationBuilder.DeleteData(
                table: "ISPs",
                keyColumn: "Id",
                keyValue: new Guid("dc1be64e-ae55-4cd7-a35a-198da28fd639"));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ISPs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "ISPs",
                columns: new[] { "Id", "Contact_No", "CreatedAt", "Description", "Email", "Image", "LastUpdated", "Lowest_Price", "Max_Speed", "Name", "Rating", "Status", "Url" },
                values: new object[,]
                {
                    { new Guid("60e4085d-5204-44c1-8cd0-9fcfd0b3ba80"), "6789012345", new DateTime(2024, 5, 20, 14, 22, 27, 85, DateTimeKind.Utc).AddTicks(2121), "Jio is an Indian telecommunications company and a subsidiary of Jio Platforms, headquartered in Navi Mumbai, Maharashtra. It operates a national LTE network with coverage across all 22 telecom circles. Jio offers 5G, 4G and 4G+ services all over India and 5G service almost All Over India. Its 6G service is in the works. Jio soft launched on 27 December 2015 with a beta for partners and employees, and became publicly available on 5 September 2016.", "info@airtel.com", "https://jep-asset.akamaized.net/cms/assets/jiofiber/discover/excited-to-experience-jiofiber.png", new DateTime(2024, 5, 20, 14, 22, 27, 85, DateTimeKind.Utc).AddTicks(2122), 599, "Upto 30 Mbps", "Jio", 4.2000000000000002, 0, "https://www.jio.com/fiber" },
                    { new Guid("695681f5-5a26-4bdf-9b62-dc506e54610a"), "1234567890", new DateTime(2024, 5, 20, 14, 22, 27, 85, DateTimeKind.Utc).AddTicks(2101), "Bharat Sanchar Nigam Limited | Explore our latest offers and hot tariffs of various services and get activated instantly online. BSNL offers Telecom Training for Students and Professionals. BSNL offers Spaces/Land available for Rent/sale. Mobile Explore various Promotional Offers for Mobile.", "info@bsnl.com", "https://www.bsnl.co.in/Logo-New.png", new DateTime(2024, 5, 20, 14, 22, 27, 85, DateTimeKind.Utc).AddTicks(2102), 399, "30 Mbps", "BSNL", 4.0, 0, "https://www.bsnl.co.in/" },
                    { new Guid("74de6f6e-2822-48b9-86de-7d55343ffe43"), "2134789045", new DateTime(2024, 5, 20, 14, 22, 27, 85, DateTimeKind.Utc).AddTicks(2119), "Bharti Airtel Limited, commonly known as Airtel, is an Indian multinational telecommunications services company based in New Delhi. It operates in 18 countries across South Asia and Africa, as well as the Channel Islands. Currently, Airtel provides 5G, 4G and LTE Advanced services throughout India. Currently offered services include fixed-line broadband, and voice services depending upon the country of operation.", "info@airtel.com", "https://assets.airtel.in/static-assets/new-home/img/airtel-red.svg", new DateTime(2024, 5, 20, 14, 22, 27, 85, DateTimeKind.Utc).AddTicks(2119), 499, "Upto 40 Mbps", "Airtel", 4.4000000000000004, 0, "https://www.airtel.in/" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ISPs",
                keyColumn: "Id",
                keyValue: new Guid("60e4085d-5204-44c1-8cd0-9fcfd0b3ba80"));

            migrationBuilder.DeleteData(
                table: "ISPs",
                keyColumn: "Id",
                keyValue: new Guid("695681f5-5a26-4bdf-9b62-dc506e54610a"));

            migrationBuilder.DeleteData(
                table: "ISPs",
                keyColumn: "Id",
                keyValue: new Guid("74de6f6e-2822-48b9-86de-7d55343ffe43"));

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ISPs");

            migrationBuilder.InsertData(
                table: "ISPs",
                columns: new[] { "Id", "Contact_No", "CreatedAt", "Description", "Email", "Image", "LastUpdated", "Lowest_Price", "Max_Speed", "Name", "Rating", "Url" },
                values: new object[,]
                {
                    { new Guid("7007b758-e569-44b1-b6d1-e89ff731132f"), "2134789045", new DateTime(2024, 5, 14, 16, 39, 28, 807, DateTimeKind.Utc).AddTicks(106), "Bharti Airtel Limited, commonly known as Airtel, is an Indian multinational telecommunications services company based in New Delhi. It operates in 18 countries across South Asia and Africa, as well as the Channel Islands. Currently, Airtel provides 5G, 4G and LTE Advanced services throughout India. Currently offered services include fixed-line broadband, and voice services depending upon the country of operation.", "info@airtel.com", "https://assets.airtel.in/static-assets/new-home/img/airtel-red.svg", new DateTime(2024, 5, 14, 16, 39, 28, 807, DateTimeKind.Utc).AddTicks(106), 499, "Upto 40 Mbps", "Airtel", 4.4000000000000004, "https://www.airtel.in/" },
                    { new Guid("a6b16136-5acf-4d26-9d3e-50686d3f55c7"), "1234567890", new DateTime(2024, 5, 14, 16, 39, 28, 807, DateTimeKind.Utc).AddTicks(86), "Bharat Sanchar Nigam Limited | Explore our latest offers and hot tariffs of various services and get activated instantly online. BSNL offers Telecom Training for Students and Professionals. BSNL offers Spaces/Land available for Rent/sale. Mobile Explore various Promotional Offers for Mobile.", "info@bsnl.com", "https://www.bsnl.co.in/Logo-New.png", new DateTime(2024, 5, 14, 16, 39, 28, 807, DateTimeKind.Utc).AddTicks(87), 399, "30 Mbps", "BSNL", 4.0, "https://www.bsnl.co.in/" },
                    { new Guid("dc1be64e-ae55-4cd7-a35a-198da28fd639"), "6789012345", new DateTime(2024, 5, 14, 16, 39, 28, 807, DateTimeKind.Utc).AddTicks(109), "Jio is an Indian telecommunications company and a subsidiary of Jio Platforms, headquartered in Navi Mumbai, Maharashtra. It operates a national LTE network with coverage across all 22 telecom circles. Jio offers 5G, 4G and 4G+ services all over India and 5G service almost All Over India. Its 6G service is in the works. Jio soft launched on 27 December 2015 with a beta for partners and employees, and became publicly available on 5 September 2016.", "info@airtel.com", "https://jep-asset.akamaized.net/cms/assets/jiofiber/discover/excited-to-experience-jiofiber.png", new DateTime(2024, 5, 14, 16, 39, 28, 807, DateTimeKind.Utc).AddTicks(110), 599, "Upto 30 Mbps", "Jio", 4.2000000000000002, "https://www.jio.com/fiber" }
                });
        }
    }
}
