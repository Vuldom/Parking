using Microsoft.EntityFrameworkCore.Migrations;

namespace Educo.Parking.Data.Migrations
{
    public partial class AddCarData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO[dbo].[Cars]([StateNumber], [Manufacturer], [Model], [Color], [Year]) VALUES(N'1346-AA7', N'Kia', N'Seed', N'brown', 2016)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Cars WHERE StateNumber = '1346-AA7'");
        }

    }
}
