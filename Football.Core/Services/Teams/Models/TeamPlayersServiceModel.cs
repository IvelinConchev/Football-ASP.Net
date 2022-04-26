namespace Football.Core.Services.Teams.Models
{
    using System;

    public class TeamPlayersServiceModel
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public string FirstName { get; init; }
        public string LastName { get; init; }
        //public string Team { get; init; }
    }
}
