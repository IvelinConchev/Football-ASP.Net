namespace Football.Core.Contracts
{
    using System;
    using Football.Core.Models.Leagues;
    using Football.Core.Services.Leagues;

    public interface ILeagueService
    {
        LeagueQueryServiceModel All(
            string name,
            string searchTerm,
            LeagueSorting sorting,
            int currentPage,
            int leaguesPerPage);

        LeagueDetailsServiceModel Details(Guid leagueId);

        Guid Create(
            string name,
            string image,
            string description,
            Guid teamId,
            Guid managerId);

        bool Edit(
            Guid id,
            string name,
            string image,
            string description,
            Guid guidId);

        IEnumerable<LeagueServiceModel> ByUser(string userId);

        bool IsByManager(Guid leagueId, Guid managerId);

        IEnumerable<string> AllNames();

        IEnumerable<LeagueTeamServiceModel> AllTeams();

        bool TeamExists(Guid leagueId);
    }
}
