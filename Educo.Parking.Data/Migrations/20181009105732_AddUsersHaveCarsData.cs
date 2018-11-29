using Microsoft.EntityFrameworkCore.Migrations;

namespace Educo.Parking.Data.Migrations
{
    public partial class AddUsersHaveCarsData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [dbo].[UsersHaveCars] ([IdUser], [IdCar]) VALUES ((SELECT dbo.AspNetUsers.Id FROM dbo.AspNetUsers WHERE dbo.AspNetUsers.Email = N'm@gmail.com'), (SELECT IdCar FROM Cars WHERE StateNumber = '1346-AA7'))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM UsersHaveCars WHERE IdUser = (SELECT dbo.AspNetUsers.Id FROM dbo.AspNetUsers WHERE dbo.AspNetUsers.Email = N'm@gmail.com')) AND (IdCar = (SELECT IdCar FROM Cars WHERE StateNumber = '1346-AA7'))");
        }
    }
}
