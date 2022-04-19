namespace Football.Core.Services.Leagues.Models
{
    using System;

    public class LeagueDetailsServiceModel : LeagueServiceModel
    {
        public string Description { get; init; }

        public Guid TeamId { get; init; }

        public Guid ManagerId { get; init; }

        public string ManagerName { get; init; }

        public string UserId { get; init; }
    }
}
