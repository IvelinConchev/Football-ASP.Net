namespace Football.Core.Models.Cities
{
    using Football.Core.Services.Cities;
    using System.ComponentModel.DataAnnotations;

    public class AllCityQueryModel
    {
        public const int CityPerPage = 3;

        public string Name { get; init; }


        [Display(Name = "Search by text")]
        public string SearchTerm { get; init; }

        public CitySorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalCities { get; set; }

        public IEnumerable<string> Names { get; set; }

        public IEnumerable<CityServiceModel> Cities { get; set; }
    }
}
