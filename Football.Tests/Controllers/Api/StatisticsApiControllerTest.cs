namespace Football.Tests.Controllers.Api
{
    using Football.Controllers.Api;
    using Football.Tests.Mocks;
    using Xunit;

    public class StatisticsApiControllerTest
    {
        [Fact]
        public void GetStatisticsShouldReturnTotalStatistics()
        {
            //Arrange
            var statisticsController = new StatisticsApiController(StatisticsServiceMock.Instance);

            //Act
            var result = statisticsController.GetStatistics();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(10, result.TotalTeams);
            Assert.Equal(5, result.TotalUsers);
        }
    }
}
