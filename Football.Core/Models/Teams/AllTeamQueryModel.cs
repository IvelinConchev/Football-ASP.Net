namespace Football.Core.Models.Teams
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Football.Core.Services.Teams;
    using Football.Core.Services.Teams.Models;

    public class AllTeamQueryModel
    {
        public const int TeamPerPage = 3;

        [Display(Name = "Отбор")]
        public string Team { get; init; }

        [Display(Name = "Търсене по дума")]
        public string SearchTerm { get; init; }

        [Display(Name = "Сортиране")]
        public TeamSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalTeams { get; set; }

        public IEnumerable<string> TeamCities { get; set; }

        public IEnumerable<TeamServiceModel> Teams { get; set; }
    }
}
