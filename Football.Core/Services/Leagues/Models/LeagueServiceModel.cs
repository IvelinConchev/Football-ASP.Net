namespace Football.Core.Services.Leagues.Models
{
    using Football.Core.Contracts;
    using System;

    public class LeagueServiceModel : ILeagueModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public string TeamName { get; set; }

        public bool IsPublic { get; init; }
    }
}
