using Microsoft.EntityFrameworkCore.Migrations;

namespace Educo.Parking.Data.Migrations
{
    public partial class AddParkingHistoryData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO[dbo].[ParkingHistory](IdParking, IdUser, IdCar, Arrival, Departure) VALUES((SELECT IdParking FROM Parkings WHERE Name = 'Brest'), (SELECT dbo.AspNetUsers.Id FROM dbo.AspNetUsers WHERE dbo.AspNetUsers.Email = N'm@gmail.com'), (SELECT IdCar FROM Cars WHERE StateNumber = '1346-AA7'), GETDATE(), GETDATE()+1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM ParkingHistory WHERE (IdParking = (SELECT IdParking FROM Parkings WHERE Name = 'Brest')) AND (IdUser = (SELECT dbo.AspNetUsers.Id FROM dbo.AspNetUsers WHERE dbo.AspNetUsers.Email = N'm@gmail.com')) AND (IdCar = (SELECT IdCar FROM Cars WHERE StateNumber = '1346-AA7'))");
        }
    }
}
