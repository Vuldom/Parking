using Microsoft.EntityFrameworkCore.Migrations;

namespace Educo.Parking.Data.Migrations
{
    public partial class AddParkingsData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [dbo].[Parkings] ([Name], [Latitude], [Longitude]) VALUES (N'Petersburg', 53.965617, 27.615311)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Parkings] ([Name], [Latitude], [Longitude]) VALUES (N'Moscow', 53.952647, 27.705878)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Parkings] ([Name], [Latitude], [Longitude]) VALUES (N'Brest', 53.847607, 27.475219)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Parkings] ([Name], [Latitude], [Longitude]) VALUES (N'Mohilev', 53.861255, 27.669528)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Parkings WHERE Name = 'Petersburg'");
            migrationBuilder.Sql("DELETE FROM Parkings WHERE Name = 'Moscow'");
            migrationBuilder.Sql("DELETE FROM Parkings WHERE Name = 'Brest'");
            migrationBuilder.Sql("DELETE FROM Parkings WHERE Name = 'Mohilev'");
        }
    }
}
