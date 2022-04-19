namespace Football.Core.Services.Cities.Models
{
    public class CityServiceModel
    {
        public Guid Id { get; set; } 

        public string Name { get; set; }

        public string PostCode { get; set; }

        public string Image { get; set; }

        public string Desctription { get; set; }

        public string TeamName { get; set; }

        //public string StadiumName { get; set; }
        //public IList<TeamCity> TeamCities { get; set; } = new List<TeamCity>();

        //public ICollection<StadiumCity> StadiumCities { get; set; } = new List<StadiumCity>();
    }
}
