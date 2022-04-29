namespace Football.Tests.Services
{
    using Football.Core.Services.Players;
    using Football.Infrastructure.Data.Models;
    using Football.Tests.Mock;
    using System;
    using Xunit;

    public class PlayersServiceTest
    {
        [Fact]
        public void IsPlayersShouldReturnTrue()
        {
            //Arrange
            const string firstName = "Martin";
            const string middleName = "Ivanov";
            const string lastName = "Martinov";
            const string team = "Milan";
            const int age = 27;
            const double weight = 68;
            const double height = 168;
            const string image = "image.jpg";
            const int goal = 3;
            const int shirtNumber = 16;
            const string nationality = "bulgarian";
            const string description = "alabala";
            Guid positionId = Guid.NewGuid();
            Guid managerId = Guid.NewGuid();

            using var data = DatabaseMock.Instance;

            data.Players.Add(new Player { FirstName = firstName, MiddleName = middleName, LastName = lastName, Team = team, Age = age, Weight = weight, Height = height, Image = image, Goal = goal, ShirtNumber = shirtNumber, Nationality = nationality, Description = description });
            data.SaveChanges();

            var playerService = new PlayerService(data);

            //Act
            var result = playerService.Create(firstName, middleName, lastName, team, age, weight, height, image, goal, shirtNumber, nationality, description, positionId, managerId);


            //Assert
            Assert.True(true);
        }

        [Fact]
        public void IsCitiesShouldReturnFalse()
        {
            //Arrange
            const string firstName = "Martin";
            const string middleName = "Ivanov";
            const string lastName = "Martinov";
            const string team = "Milan";
            const int age = 27;
            const double weight = 68;
            const double height = 168;
            const string image = "image.jpg";
            const int goal = 3;
            const int shirtNumber = 16;
            const string nationality = "bulgarian";
            const string description = null;
            Guid positionId = Guid.NewGuid();
            Guid managerId = Guid.NewGuid();

            using var data = DatabaseMock.Instance;

            data.Players.Add(new Player { FirstName = firstName, MiddleName = middleName, LastName = lastName, Team = team, Age = age, Weight = weight, Height = height, Image = image, Goal = goal, ShirtNumber = shirtNumber, Nationality = nationality, Description = description });
            data.SaveChanges();

            var playerService = new PlayerService(data);

            //Act
            var result = playerService.Create(firstName, middleName, lastName, team, age, weight, height, image, goal, shirtNumber, nationality, description, positionId, managerId);


            //Assert
            Assert.False(false);
        }

        [Fact]
        public void IsCitiesShouldReturnDetailsFalse()
        {
            //Arrange
            const string firstName = "Martin";
            const string middleName = "Ivanov";
            const string lastName = "Martinov";
            const string team = "Milan";
            const int age = 27;
            const double weight = 68;
            const double height = 168;
            const string image = "image.jpg";
            const int goal = 3;
            const int shirtNumber = 16;
            const string nationality = "bulgarian";
            const string description = null;
            Guid positionId = Guid.NewGuid();
            Guid managerId = Guid.NewGuid();

            using var data = DatabaseMock.Instance;

            data.Players.Add(new Player { FirstName = firstName, MiddleName = middleName, LastName = lastName, Team = team, Age = age, Weight = weight, Height = height, Image = image, Goal = goal, ShirtNumber = shirtNumber, Nationality = nationality, Description = description });
            data.SaveChanges();

            var playerService = new PlayerService(data);

            //Act
            var result = playerService.Details(managerId);


            //Assert
            Assert.False(false);
        }

        [Fact]
        public void IsCitiesShouldReturnDetailsTrue()
        {
            //Arrange
            const string firstName = "Martin";
            const string middleName = "Ivanov";
            const string lastName = "Martinov";
            const string team = "Milan";
            const int age = 27;
            const double weight = 68;
            const double height = 168;
            const string image = "image.jpg";
            const int goal = 3;
            const int shirtNumber = 16;
            const string nationality = "bulgarian";
            const string description = null;
            Guid positionId = Guid.NewGuid();
            Guid managerId = Guid.NewGuid();

            using var data = DatabaseMock.Instance;

            data.Players.Add(new Player { FirstName = firstName, MiddleName = middleName, LastName = lastName, Team = team, Age = age, Weight = weight, Height = height, Image = image, Goal = goal, ShirtNumber = shirtNumber, Nationality = nationality, Description = description });
            data.SaveChanges();

            var playerService = new PlayerService(data);

            //Act
            var result = playerService.Details(managerId);


            //Assert
            Assert.True(true);
        }

        [Fact]
        public void IsCitiesShouldReturnDeleteTrue()
        {
            //Arrange
            Guid id = Guid.NewGuid();   
            const string firstName = "Martin";
            const string middleName = "Ivanov";
            const string lastName = "Martinov";
            const string team = "Milan";
            const int age = 27;
            const double weight = 68;
            const double height = 168;
            const string image = "image.jpg";
            const int goal = 3;
            const int shirtNumber = 16;
            const string nationality = "bulgarian";
            const string description = null;
            Guid positionId = Guid.NewGuid();
            Guid managerId = Guid.NewGuid();

            using var data = DatabaseMock.Instance;

            data.Players.Add(new Player { FirstName = firstName, MiddleName = middleName, LastName = lastName, Team = team, Age = age, Weight = weight, Height = height, Image = image, Goal = goal, ShirtNumber = shirtNumber, Nationality = nationality, Description = description });
            data.SaveChanges();

            var playerService = new PlayerService(data);

            //Act
            var result = playerService.Delete(id);


            //Assert
            Assert.True(true);
        }
    }
}
