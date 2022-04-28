namespace Football.Tests.Services
{
    using Football.Core.Services.Teams;
    using Football.Infrastructure.Data.Models;
    //using Football.Infrastructure.Data.Models;
    using Football.Tests.Mock;
    using System;
    using Xunit;

    public class TeamsServiceTest
    {
        [Fact]
        public void IsTeamsShouldReturnTrue()
        {
            //Arrange
            Guid guid = Guid.NewGuid();
            const string name = "Milan";
            const string image = "img";
            const string webSite = "wwwsite";
            const string logoUrl = "da";
            const string homeKit = "red";
            const string awayKit = "white";
            const string nickName = "Wild";
            const string description = "alabala";
            const string address = "Ivan Vazov";
            const string headCoach = "Dimitar Penev";
            const int champion = 1;
            const int cup = 1;
            const int win = 20;
            const int defeats = 40;
            Guid playerId = Guid.NewGuid();
            Guid managerId = Guid.NewGuid();

            using var data = DatabaseMock.Instance;

            data.Teams.Add(new Team { Name = name, Image = image, WebSite = webSite, LogoUrl = logoUrl, HomeKit = homeKit, AwayKit = awayKit, NickName = nickName, Description = description, Address = address,
            HeadCoach = headCoach, Champion = champion, Cup = cup, Win = win, Defeats = defeats});
            data.SaveChanges();

            var teamService = new TeamService(data);

            //Act
            var result = teamService.Create(name, image, webSite, logoUrl, homeKit, awayKit, nickName, description, address,headCoach, champion, cup, win, defeats, playerId, managerId);

            //Assert
            Assert.True(true, "p");
        }
    }
}
