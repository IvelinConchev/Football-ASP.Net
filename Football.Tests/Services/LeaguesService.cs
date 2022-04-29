namespace Football.Tests.Services
{
    using Football.Core.Services.Leagues;
    using Football.Infrastructure.Data.Models;
    using Football.Tests.Mock;
    using System;
    using Xunit;

    public class LeaguesService
    {
        [Fact]
        public void IsCitiesShouldReturnTrue()
        {
            //Arrange
            const string name = "Efbet";
            const string image = "image.jpg";
            const string description = "alabala";
            Guid teamId = Guid.NewGuid();
            Guid managerId = Guid.NewGuid();

            using var data = DatabaseMock.Instance;

            data.Leagues.Add(new League { Name = name, Image = image, Description = description });

            var leagueService = new LeagueService(data);

            //Act

            var result = leagueService.Create(name, image, description, teamId, managerId);

            //Assert
            Assert.True(true, "p");
        }

        [Fact]
        public void IsCitiesShouldReturnFalse()
        {
            //Arrange
            const string name = "Efbet";
            const string image = "image.jpg";
            const string description = null;
            Guid teamId = Guid.NewGuid();
            Guid managerId = Guid.NewGuid();

            using var data = DatabaseMock.Instance;

            data.Leagues.Add(new League { Name = name, Image = image, Description = description });

            var leagueService = new LeagueService(data);

            //Act

            var result = leagueService.Create(name, image, description, teamId, managerId);

            //Assert
            Assert.False(false, "p");
        }
    }
}
