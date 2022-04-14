namespace Football.Models.Cities
{
    public class CityListingViewModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }

        public string? PostCode { get; set; }

        public string? Image { get; set; }

        public string? Desctription { get; set; }

        public string? Team { get; set; }
    }
}
