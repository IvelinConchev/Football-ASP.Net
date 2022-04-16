namespace Football.Core.Services.Leagues
{
    using System.Collections.Generic;

    public class LeagueQueryServiceModel
    {
        public int CurrentPage { get; init; }

        public int LeaguesPerPage { get; init; }

        public int TotalLeagues { get; init; }

        public IEnumerable<LeagueServiceModel> Leagues { get; init; }
    }
}
