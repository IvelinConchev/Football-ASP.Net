namespace Football.Core.Services.Leagues.Models
{
    using System;

    public class LeagueServiceModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public string TeamName { get; set; }
    }
}
