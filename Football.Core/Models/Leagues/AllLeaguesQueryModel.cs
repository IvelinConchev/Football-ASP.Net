namespace Football.Core.Models.Leagues
{
    using System.Collections.Generic;
    using Football.Core.Services.Leagues;
    using Football.Core.Services.Leagues.Models;

    public class AllLeaguesQueryModel
    {
        public const int LeaguesPerPage = 3;

        public string Name { get; init; }

        public string SearchTerm { get; init; }

        public LeagueSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalLeagues { get; set; }

        public IEnumerable<string> Names { get; set; }

        public IEnumerable<LeagueServiceModel> Leagues { get; set; }
    }
}
