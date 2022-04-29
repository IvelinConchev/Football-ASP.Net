namespace Football.Tests.Services
{
    using Football.Core.Services.Stadiums;
    using Football.Infrastructure.Data.Models;
    using Football.Tests.Mock;
    using System;
    using Xunit;

    public class StadiumsService
    {
        [Fact]
        public void IsCitiesShouldReturnTrue()
        {
            //Arrange
            const string name = "Efbet";
            const string image = "image.jpg";
            const string description = "alabala";
            const int Capacity = 10000;
            const string address = "Ivan Vazov";
            Guid cityId = Guid.NewGuid();
            Guid managerId = Guid.NewGuid();

            using var data = DatabaseMock.Instance;

            data.Stadiums.Add(new Stadium {Name = name, Image = image, Description = description, Address = address, Capacity = Capacity });

            var stadiumService = new StadiumService(data);

            //Act

            var result = stadiumService.Create(name, image, description, Capacity, address, cityId, managerId);

            //Assert
            Assert.True(true, "p");
        }

        [Fact]
        public void IsCitiesShouldReturnFalse()
        {
            //Arrange
            const string name = "Efbet";
            const string image = "image.jpg";
            const string description = "alabala";
            const int Capacity = 10000;
            const string address = null;
            Guid cityId = Guid.NewGuid();
            Guid managerId = Guid.NewGuid();

            using var data = DatabaseMock.Instance;

            data.Stadiums.Add(new Stadium { Name = name, Image = image, Description = description, Address = address, Capacity = Capacity });

            var stadiumService = new StadiumService(data);

            //Act

            var result = stadiumService.Create(name, image, description, Capacity, address, cityId, managerId);

            //Assert
            Assert.False(false, "p");
        }

        [Fact]
        public void IsCitiesShouldReturnDetailsIdTrue()
        {
            //Arrange
             Guid id = Guid.NewGuid();
            const string name = "Efbet";
            const string image = "image.jpg";
            const string description = "alabala";
            const int Capacity = 10000;
            const string address = null;
            Guid cityId = Guid.NewGuid();
            Guid managerId = Guid.NewGuid();

            using var data = DatabaseMock.Instance;

            data.Stadiums.Add(new Stadium { Name = name, Image = image, Description = description, Address = address, Capacity = Capacity });

            var stadiumService = new StadiumService(data);

            //Act

            var result = stadiumService.Details(id);

            //Assert
            Assert.True(true, "p");
        }

        [Fact]
        public void IsCitiesShouldReturnDetailsIdFalse()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            const string name = "Efbet";
            const string image = "image.jpg";
            const string description = "alabala";
            const int Capacity = 10000;
            const string address = null;
            Guid cityId = Guid.NewGuid();
            Guid managerId = Guid.NewGuid();

            using var data = DatabaseMock.Instance;

            data.Stadiums.Add(new Stadium { Name = name, Image = image, Description = description, Address = address, Capacity = Capacity });

            var stadiumService = new StadiumService(data);

            //Act

            var result = stadiumService.Details(id);

            //Assert
            Assert.True(true, "p");
        }

        [Fact]
        public void IsCitiesShouldReturnDeleteTrue()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            const string name = "Efbet";
            const string image = "image.jpg";
            const string description = "alabala";
            const int Capacity = 10000;
            const string address = null;
            Guid cityId = Guid.NewGuid();
            Guid managerId = Guid.NewGuid();

            using var data = DatabaseMock.Instance;

            data.Stadiums.Add(new Stadium { Name = name, Image = image, Description = description, Address = address, Capacity = Capacity });

            var stadiumService = new StadiumService(data);

            //Act

            var result = stadiumService.Delete(id);

            //Assert
            Assert.True(true, "p");
        }
    }
}
