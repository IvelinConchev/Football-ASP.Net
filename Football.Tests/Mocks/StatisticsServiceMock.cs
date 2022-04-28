namespace Football.Tests.Mock
{
    using Football.Core.Contracts;
    using Football.Core.Services.Statistics.Models;
    using Moq;

    public static class StatisticsServiceMock
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
                        TotalUsers = 5,
                        TotalTeams = 10
                    });

                return statisticsServiceMock.Object;
            }
        }
    }
}
