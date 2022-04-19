namespace Football.Core.Services.Cities.Models
{
    using System.Collections.Generic;

    public class CityQueryServiceModel
    {
        public int CurrentPage { get; init; }

        public int CitiesPerPage { get; init; }

        public int TotalCities { get; init; }

        public IEnumerable<CityServiceModel> Cities { get; init; }
    }
}
