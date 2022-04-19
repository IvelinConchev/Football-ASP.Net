namespace Football.Core.Models.Stadiums
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Football.Core.Services.Stadiums;

    public class AllStadiumsQueryModel
    {
        public const int StadiumsPerPage = 3;

        public string Name { get; init; }


        [Display(Name = "Search by text")]
        public string SearchTerm { get; init; }

        public StadiumSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalStadiums { get; set; }

        public IEnumerable<string> Names { get; set; }

        public IEnumerable<StadiumServiceModel> Stadiums { get; set; }
    }
}
