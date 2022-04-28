namespace Football.Tests.Controllers
{
    using Football.Controllers;
    using Football.Core.Services.Statistics;
    using Football.Core.Services.Teams;
    using Football.Models.Home;
    using Football.Tests.Mock;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using Moq;
    using Xunit;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnViewWithCorrectModel()
        {
            var data = DatabaseMock.Instance;
            //Arrange
            var teamService = new TeamService(data);
            var cache = new Mock<IMemoryCache>();
            var statisticsServise = new StatisticsService(data);

            var homeController = new HomeController(teamService, statisticsServise, cache.Object);

            //Act
            var result = homeController.Index();

            //Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);

            var model = viewResult.Model;

            var indexViewModel = Assert.IsType<IndexViewModel>(model);
        }

        [Fact]
        public void ErrorShouldReturnView()
        {
            //Arrange
            var homeController = new HomeController(null, null, null);

            //Act
            var result = homeController.Error();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
    }
}
