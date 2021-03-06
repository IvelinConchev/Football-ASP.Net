namespace Football.Tests.Services
{
    using Football.Core.Services.Managers;
    using Football.Infrastructure.Data.Models;
    using Football.Tests.Mock;
    using Xunit;

    public class ManagerServiceTest
    {
        [Fact]
        public void IsManagerShouldReturnTrueWhenUserIsManager()
        {
            //Arrange
            const string userId = "TestUserId";
            const string name = "Petar Mihov";
            const string phoneNumber = "+359888888888";
            using var data = DatabaseMock.Instance;

            data.Managers.Add(new Manager { UserId = userId, Name = name, PhoneNumber = phoneNumber });
            data.SaveChanges();

            var managerService = new ManagerService(data);

            //Act
            var result = managerService.IsManager(userId);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void IsManagerShouldReturnFalseWhenUserIsNotManager()
        {
            //Arrange
            const string userId = "TestUserId";
            const string name = "Petar Mihov";
            const string phoneNumber = "+359888888888";
            using var data = DatabaseMock.Instance;

            data.Managers.Add(new Manager { UserId = userId, Name = name, PhoneNumber = phoneNumber });
            data.SaveChanges();

            var managerService = new ManagerService(data);

            //Act
            var result = managerService.IsManager("AnotherUserId");

            //Assert
            Assert.False(result);

        }
    }
}
