namespace Football.Models.Stadiums
{
    public class StadiumListingViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public string Decription { get; set; }

        public int Capacity { get; set; }

        public string Address { get; set; }

        public string City { get; set; }
    }
}
