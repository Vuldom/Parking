using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Educo.Parking.Data.Migrations
{
    public partial class AddUserAndRoleData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Add Role
            migrationBuilder.Sql($"INSERT INTO dbo.AspNetRoles (Id, ConcurrencyStamp, Name, NormalizedName) VALUES (N'{Guid.NewGuid().ToString()}', N'{Guid.NewGuid().ToString()}', N'Administrator', UPPER(N'Administrator'))");
            migrationBuilder.Sql($"INSERT INTO dbo.AspNetRoles (Id, ConcurrencyStamp, Name, NormalizedName) VALUES (N'{Guid.NewGuid().ToString()}', N'{Guid.NewGuid().ToString()}', N'User', UPPER(N'User'))");
            migrationBuilder.Sql($"INSERT INTO dbo.AspNetRoles (Id, ConcurrencyStamp, Name, NormalizedName) VALUES (N'{Guid.NewGuid().ToString()}', N'{Guid.NewGuid().ToString()}', N'Manager', UPPER(N'Manager'))");

            //Add Users
            migrationBuilder.Sql($"INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [Lastname], [UserPhoto]) " +
                $"VALUES (N'25a86786-2d5e-46d6-b629-0fd00cc0497f', N'm@gmail.com', N'M@GMAIL.COM', N'm@gmail.com', N'M@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEJ48A5OBNFo2H0wmqSAmI3EGTOlavqMuyp99gdUReymSqrLxQ/BTSEPVNLD3XOiWqw==', N'62SZ4V3UU47TYBKV2KZD5Q4J57T2NFQM', N'7380cdd0-d0ee-4352-ad9e-6e541dce214f', NULL, 0, 0, NULL, 1, 0, N'Maryia', N'Paliashchuk', NULL)");

            //Add UserRoles
            migrationBuilder.Sql("INSERT INTO dbo.AspNetUserRoles (UserId, RoleId) VALUES ((SELECT dbo.AspNetUsers.Id FROM dbo.AspNetUsers WHERE dbo.AspNetUsers.Email = N'm@gmail.com'), (SELECT dbo.AspNetRoles.Id FROM dbo.AspNetRoles WHERE dbo.AspNetRoles.Name = N'Administrator'))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM dbo.AspNetUsers WHERE UserName = 'm@gmail.com'");
            migrationBuilder.Sql("DELETE FROM dbo.AspNetRoles WHERE Name = 'Administrator'");
            migrationBuilder.Sql("DELETE FROM dbo.AspNetRoles WHERE Name = 'User'");
            migrationBuilder.Sql("DELETE FROM dbo.AspNetRoles WHERE Name = 'Manager'");
        }
    }
}
