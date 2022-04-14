namespace Football.Models.Leagues
{
    using System.ComponentModel.DataAnnotations;

    public class LeagueTeamsViewModel
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }
    }
}
