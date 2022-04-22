namespace Football.Core.Contracts
{
    using Football.Core.Models.Leagues;
    using Football.Core.Services.Leagues.Models;
    using System;

    public interface ILeagueService
    {
        LeagueQueryServiceModel All(
            string name = null,
            string searchTerm = null,
            LeagueSorting sorting = LeagueSorting.Name,
            int currentPage = 1,
            int leaguesPerPage = int.MaxValue,
            bool publicOnly = true);

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
            Guid guidId,
            bool isPublic);

        Guid Delete(Guid id);

        IEnumerable<LeagueServiceModel> ByUser(string userId);

        bool IsByManager(Guid leagueId, Guid managerId);

        void ChangeVisibility(Guid teamId);

        IEnumerable<string> AllNames();

        IEnumerable<LeagueTeamServiceModel> AllTeams();

        bool TeamExists(Guid leagueId);
    }
}
