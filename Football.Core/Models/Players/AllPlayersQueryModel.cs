namespace Football.Core.Models.Players
{
    using System.ComponentModel.DataAnnotations;
    using Football.Core.Services.Players;
    using Football.Core.Services.Players.Models;

    public class AllPlayersQueryModel
    {
        public const int PlayersPerPage = 3;

        [Display(Name = "Играч")]
        public string Team { get; init; }

        [Display(Name = "Търсене по дума")]
        public string SearchTerm { get; init; }

        [Display(Name = "Сортиране")]
        public PlayerSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalPlayers { get; set; }

        public IEnumerable<string> Teams { get; set; }

        public IEnumerable<PlayerServiceModel> Players { get; set; }
    }
}

