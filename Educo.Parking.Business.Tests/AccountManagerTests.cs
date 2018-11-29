using System;
using Educo.Parking.Business.Managers;
using Xunit;

namespace Educo.Parking.Business.Tests
{
    public class AccountManagerTests
    {
        [Fact]
        public void GetPasswordReturnsCorrectPassword()
        {
            AccountManager accountManager = new AccountManager();

            //act
            string password = accountManager.GetPassword();

            //Assert
            Assert.NotNull(password);
            Assert.Equal(5, password.Length);
        }
    }
}
