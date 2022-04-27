namespace Football.Core.Models.Cities
{
    using Football.Core.Services.Cities.Models;
    using System.ComponentModel.DataAnnotations;

    public class AllCityQueryModel
    {
        public const int CityPerPage = 3;

        [Display(Name = "Град")]
        public string Name { get; init; }

        [Display(Name = "Търси по дума")]
        public string SearchTerm { get; init; }

        [Display(Name = "Сортиране")]
        public CitySorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalCities { get; set; }

        public IEnumerable<string> Names { get; set; }

        public IEnumerable<CityServiceModel> Cities { get; set; }
    }
}
