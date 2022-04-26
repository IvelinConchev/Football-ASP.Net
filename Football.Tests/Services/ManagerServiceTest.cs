namespace Football.Tests.Services
{
    using Football.Core.Services.Managers;
    using Football.Infrastructure.Data.Models;
    using Football.Tests.Mocks;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    public class ManagerServiceTest
    {
        [Fact]
        public void IsDealerShouldReturnTrueWhenUserIsManager()
        {
            //Arrange
            const string userId = "TestUserId";
            using var data = DatabaseMock.Instance;

            data.Managers.Add(new Manager { UserId = userId, Name = "09009", PhoneNumber="89070" });
            data.SaveChanges();

            var managerService = new ManagerService(data);

            //Act
            var result = managerService.IsManager(userId);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void IsDealerShouldReturnTrueWhenUserIsNotManager()
        {
            //Arrange
            const string userId = "TestUserId";
            using var data = DatabaseMock.Instance;

            data.Managers.Add(new Manager { UserId = userId, Name = "09009", PhoneNumber = "89070" });
            data.SaveChanges();

            var managerService = new ManagerService(data);

            //Act
            var result = managerService.IsManager("AnotherUserId");

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void IsDealerShouldReturnTrueWhenUserIsIdByUserNotUser()
        {
            //Arrange
            const string userId = "TestUserId";
            using var data = DatabaseMock.Instance;

            data.Managers.Add(new Manager { UserId = userId, Name = "jj", PhoneNumber = "444"});
            data.SaveChanges();

            var managerService = new ManagerService(data);

            //Act
            var result = managerService.IdByUser(userId);

            //Assert
            Assert.False(false);
        }
    }
}
