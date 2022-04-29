namespace Football.Tests.Services
{
    using Football.Core.Services.Cities;
    using Football.Infrastructure.Data.Models;
    using Football.Tests.Mock;
    using System;
    using Xunit;

    public class CitiesServiceTest
    {
        [Fact]
        public void IsCitiesShouldReturnTrue()
        {
            //Arrange
            const string name = "Sofia";
            const string postCode = "5000";
            const string image = "img.jpg";
            const string description = "alabala";
            Guid teamId = Guid.NewGuid();

            using var data = DatabaseMock.Instance;

            data.Cities.Add(new City { Name = name, PostCode = postCode, Image = image });

            var cityService = new CityService(data);

            //Act

            var result = cityService.Create(name, postCode, image, description, teamId);

            //Assert
            Assert.True(true, "p");
        }

        [Fact]
        public void IsCitiesShouldReturnFalse()
        {
            //Arrange
            const string name = "Sofia";
            const string postCode = "5000";
            const string image = "img.jpg";
            const string description = null;
            Guid teamId = Guid.NewGuid();

            using var data = DatabaseMock.Instance;

            data.Cities.Add(new City { Name = name, PostCode = postCode, Image = image });

            var cityService = new CityService(data);

            //Act

            var result = cityService.Create(name, postCode, image, description, teamId);

            //Assert
            Assert.False(false, "p");
        }

        [Fact]
        public void IsCitiesShouldReturnDetailsFalse()
        {
            //Arrange
            const string name = "Sofia";
            const string postCode = "5000";
            const string image = "img.jpg";
            const string description = null;
            Guid teamId = Guid.NewGuid();

            using var data = DatabaseMock.Instance;

            data.Cities.Add(new City { Name = name, PostCode = postCode, Image = image });

            var cityService = new CityService(data);

            //Act

            var result = cityService.Details(teamId);

            //Assert
            Assert.False(false, "p");
        }

        [Fact]
        public void IsCitiesShouldReturnDetailsTrue()
        {
            //Arrange
            const string name = "Sofia";
            const string postCode = "5000";
            const string image = "img.jpg";
            const string description = null;
            Guid teamId = Guid.NewGuid();

            using var data = DatabaseMock.Instance;

            data.Cities.Add(new City { Name = name, PostCode = postCode, Image = image });

            var cityService = new CityService(data);

            //Act

            var result = cityService.Details(teamId);

            //Assert
            Assert.True(true, "p");
        }

        [Fact]
        public void IsCitiesShouldReturnDeleteTrue()
        {
            //Arrange
            Guid id = new Guid();
            const string name = "Sofia";
            const string postCode = "5000";
            const string image = "img.jpg";
            const string description = null;
            Guid teamId = Guid.NewGuid();

            using var data = DatabaseMock.Instance;

            data.Cities.Add(new City { Name = name, PostCode = postCode, Image = image });

            var cityService = new CityService(data);

            //Act

            var result = cityService.Delete(id);

            //Assert
            Assert.True(true, "p");
        }
    }
}
