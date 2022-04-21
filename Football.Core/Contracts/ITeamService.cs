namespace Football.Core.Contracts
{
    using Football.Core.Models.Teams;
    using Football.Core.Services.Teams.Models;
    using System;

    public interface ITeamService
    {
        TeamQueryServiceModel All(
            string name = null,
            string searchTerm = null,
            TeamSorting sorting = TeamSorting.Champion,
            int currentPage = 1,
            int teamsPerPage = int.MaxValue,
            bool publicOnly = true);

        IEnumerable<LatestTeamServiceModel> Latest();

        TeamDetailsServiceModel Details(Guid teamId);

        Guid Create(
            string name,
            string image,
            string webSite,
            string logoUrl,
            string homeKit,
            string awayKit,
            string nickName,
            string description,
            string address,
            string headCoach,
            int champion,
            int cup,
            int win,
            int defeats,
            Guid playerId,
            Guid managerId);

        bool Edit(Guid id,
            string name,
            string image,
            string webSite,
            string logoUrl,
            string homeKit,
            string awayKit,
            string nickName,
            string description,
            string address,
            string headCoach,
            int champion,
            int cup,
            int win,
            int defeats,
            Guid playerId,
            bool isPublic);

        IEnumerable<TeamServiceModel> ByUser(string userId);

        bool IsByManager(Guid teamId, Guid managerId);

        void ChangeVisibility(Guid teamId);  

        IEnumerable<string> AllPlayers();

        IEnumerable<TeamPlayersServiceModel> AllTeams();

        bool PlayerExists(Guid playerId);
    }
}
