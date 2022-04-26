namespace Football.Tests.Mocks
{
    using Football.Core.Contracts;
    using Football.Core.Services.Statistics.Models;
    using Moq;

    public class StatisticsServiceMock
    {
        public static IStatisticsService Instance
        {
            get
            {
                var statisticsServiceMock = new Mock<IStatisticsService>();

                statisticsServiceMock
                    .Setup(s => s.Total())
                    .Returns(new StatisticsServiceModel
                    {
                        TotalTeams = 5,
                        TotalUsers = 10
                    });
                return statisticsServiceMock.Object;
            }
        }
    }
}
