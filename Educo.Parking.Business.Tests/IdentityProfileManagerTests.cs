using Educo.Parking.Business.Managers.Profile;
using Educo.Parking.Business.Tests.Fakes;
using Educo.Parking.Business.Tests.Fakes.Managers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Educo.Parking.Business.Tests
{
    public class IdentityProfileManagerTests
    {
        private readonly FakeIdentityContextFactory fakeIdentityContext;
        private User[] users =
            {
            new User { Username = "korostelev", Email = "korostelev@unibel.by", Password = "123456" },
            new User { Username = "inissoft", Email = "korostelev@inissoft.by", Password = "654321" }
        };

        public IdentityProfileManagerTests()
        {
            string databaseName = Guid.NewGuid().ToString();
            fakeIdentityContext = new FakeIdentityContextFactory(databaseName);
        }

        [Theory]
        [InlineData("korostelev")]
        [InlineData("inissoft")]
        public async Task GetUserAsync(string username)
        {
            //Arrange
            IIdentityProfileManager profileManager = new FakeProfileManager(fakeIdentityContext.UserManager, users);
            //Act
            User user = await profileManager.GetUserAsync(username);
            //Assert
            Assert.NotNull(user);
            Assert.Equal(username, user.Username);
        }

        [Fact]
        public async Task UserUpdateAsync()
        {
            //Arrange
            IIdentityProfileManager profileManager = new FakeProfileManager(fakeIdentityContext.UserManager, users);
            User user = new User { Username = "korostelev", Email = "aaa@aa.aa" };
            //Act
            IdentityResult result = await profileManager.UserUpdateAsync(user);
            //Assert
            Assert.True(result.Succeeded);
        }

        [Theory]
        [InlineData("korostelev", "123456", "777777")]
        [InlineData("inissoft", "654321", "111111")]
        public async Task ChangePasswordAsyncOk(string userName, string currentPassword, string newPassword)
        {
            //Arrange
            IIdentityProfileManager profileManager = new FakeProfileManager(fakeIdentityContext.UserManager, users);
            //Act
            IdentityResult result = await profileManager.ChangePasswordAsync(userName, currentPassword, newPassword);
            //Assert
            Assert.True(result.Succeeded);
        }

        [Theory]
        [InlineData("korostelev", "11111", "654321")]
        [InlineData("inissoft", "22222", "33333")]
        public async Task ChangePasswordAsyncFaild(string userName, string currentPassword, string newPassword)
        {
            //Arrange
            IIdentityProfileManager profileManager = new FakeProfileManager(fakeIdentityContext.UserManager, users);
            //Act
            IdentityResult result = await profileManager.ChangePasswordAsync(userName, currentPassword, newPassword);
            string error = result.Errors.FirstOrDefault().Code;
            //Assert
            Assert.False(result.Succeeded);
            Assert.Equal("PasswordMismatch", error);
        }
    }
}
