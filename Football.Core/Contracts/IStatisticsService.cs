namespace Football.Core.Contracts
{
    using Football.Core.Services.Statistics.Models;

    public interface IStatisticsService
    {
        StatisticsServiceModel Total();
    }
}
