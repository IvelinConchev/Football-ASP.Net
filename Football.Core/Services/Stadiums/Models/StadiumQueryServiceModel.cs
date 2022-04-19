namespace Football.Core.Services.Stadiums.Models
{
    public class StadiumQueryServiceModel
    {
        public int CurrentPage { get; init; }

        public int StadiumsPerPage { get; init; }

        public int TotalStadiums { get; init; }

        public IEnumerable<StadiumServiceModel> Stadiums { get; init; }
    }
}
