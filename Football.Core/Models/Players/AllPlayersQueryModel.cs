namespace Football.Core.Models.Players
{
    using System.ComponentModel.DataAnnotations;
    using Football.Core.Services.Players;

    public class AllPlayersQueryModel
    {
        public const int PlayersPerPage = 3;

        public string Team { get; init; }

        [Display(Name = "Search by text")]
        public string SearchTerm { get; init; }

        public PlayerSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalPlayers { get; set; }

        public IEnumerable<string> Teams { get; set; }

        public IEnumerable<PlayerServiceModel> Players { get; set; }
    }
}

