namespace Football.Models.Home
{
    using Football.Core.Services.Teams.Models;

    public class IndexViewModel
    {
        public int TotalPlayers { get; init; }
        public int TotalUsers { get; init; }
        public int TotalSumPlayers { get; init; }

        public int TotalTeams { get; init; }
        public int TotalSumTeams { get; init; }

        //public List<PlayerIndexViewModel> Players { get; init; }
        public IList<LatestTeamServiceModel> Teams { get; init; }
    }
}
