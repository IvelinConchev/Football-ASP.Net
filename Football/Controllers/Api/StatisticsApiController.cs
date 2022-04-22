namespace Football.Controllers.Api
{
    using Football.Infrastructure.Data;
    using Football.Models.Api.Statistics;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    [ApiController]
    [Route("api/statistics")]
    public class StatisticsApiController : ControllerBase
    {
        private readonly FootballDbContext data;

        public StatisticsApiController(FootballDbContext data)
            => this.data = data;

        [HttpGet]
        public StatisticsResponseModel GetStatistics()
        {
            var totalTeams = this.data.Teams.Count();
            var totalUsers = this.data.Users.Count();


            return new StatisticsResponseModel
            {
                TotalTeams = totalTeams,
                TotalUsers = totalUsers,
                SumTeams = 0
            };
        }
    }
}
