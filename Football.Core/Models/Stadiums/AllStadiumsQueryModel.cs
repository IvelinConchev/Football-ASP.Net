namespace Football.Core.Models.Stadiums
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Football.Core.Services.Stadiums.Models;

    public class AllStadiumsQueryModel
    {
        public const int StadiumsPerPage = 3;

        [Display(Name = "Стадион")]
        public string Name { get; init; }


        [Display(Name = "Търсене по дума")]
        public string SearchTerm { get; init; }

        [Display(Name = "Сортиране")]
        public StadiumSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalStadiums { get; set; }

        public IEnumerable<string> Names { get; set; }

        public IEnumerable<StadiumServiceModel> Stadiums { get; set; }
    }
}
