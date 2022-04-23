namespace Football.Core.Services.Cities.Models
{
    using Football.Core.Contracts;

    public class CityServiceModel : ICityModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string PostCode { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public string TeamName { get; set; }

        public bool IsPublic { get; init; }

        //public string StadiumName { get; set; }
        //public IList<TeamCity> TeamCities { get; set; } = new List<TeamCity>();

        //public ICollection<StadiumCity> StadiumCities { get; set; } = new List<StadiumCity>();
    }
}
