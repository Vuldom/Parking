using System;
using System.Linq;
using System.Threading.Tasks;
using Educo.Parking.Business.Tests.Fakes;
using Educo.Parking.Business.Tests.Fakes.Managers;
using Educo.Parking.Data;
using Microsoft.AspNetCore.Identity;
using Xunit;

namespace Educo.Parking.Business.Tests
{
    public class AuthenticationManagerTests
    {
        DataContextFactory contextFactory;
        private readonly FakeIdentityContextFactory fakeIdentityContextFactory;

        public AuthenticationManagerTests()
        {
            var databaseName = Guid.NewGuid().ToString();
            contextFactory = new TestDataContextFactory(databaseName);
            fakeIdentityContextFactory = new FakeIdentityContextFactory(databaseName);
        }

        private User[] users =
        {
            new User { Username = "korostelev", Email = "korostelev@unibel.by", Password = "123456" },
            new User { Username = "inissoft", Email = "korostelev@inissoft.by", Password = "654321" }
        };

        [Fact]
        public async Task AddUserMethodTest()
        {
            //Arrange
            string login = "Neo";
            string lastname = "Ivanov";
            string firstname = "Ivan";
            string pass = "111";
            string email = "neo@gmail.com";
            AuthenticationManager manager = new FakeAuthenticationManager(contextFactory, fakeIdentityContextFactory.UserManager, users);
            //Act
            IdentityResult result = await manager.AddUser(login, lastname, firstname, pass, email);
            //Assert
            Assert.True(result.Succeeded);
        }

        [Theory]
        [InlineData("korostelev", "¿", "¿", "korostelev@unibel.by", "123", "DuplicateUserName")]
        [InlineData("‡‡‡", "¿", "¿", "korostelev@unibel.by", "12", "PasswordTooShort")]
        [InlineData("‡‡‡", "¿", "¿", "ÙÙÙ", "123", "InvalidUserName")]
        public async Task AddUserFailed(string login, string lastname, string firstname, string email, string pass, string error)
        {
            //Arrange
            AuthenticationManager manager = new FakeAuthenticationManager(contextFactory, fakeIdentityContextFactory.UserManager, users);
            //Act
            IdentityResult result = await manager.AddUser(login, lastname, firstname, pass, email);
            //Assert
            Assert.False(result.Succeeded);
            Assert.Equal(error, result.Errors.FirstOrDefault().Code);
        }

        [Fact]
        public void RemoveUserTest()
        {
            using (ParkingDBContext context = contextFactory.CreateDbContext())
            {
                ApplicationUser entity = new ApplicationUser();
                entity.Email = "tri@gmail.com";
                entity.FirstName = "Trinity";
                entity.Lastname = "Matrix";
                entity.UserName = "tri1999";
                context.Users.Add(entity);
                context.SaveChanges();
            }

            User user = new User();
            user.Username = "tri1999";
            AuthenticationManager fakeAuthentication = new AuthenticationManager(contextFactory, fakeIdentityContextFactory.UserManager); 
            bool removed = fakeAuthentication.RemoveUser(user);
            Assert.True(removed);
        }
    }
}
