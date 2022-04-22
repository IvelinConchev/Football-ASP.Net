namespace Football.Core.Contracts
{
    using System;
    using System.Collections.Generic;
    using Football.Core.Models.Players;
    using Football.Core.Services.Players.Models;
    public interface IPlayerService
    {
        PlayerQueryServiceModel All(
            string team = null,
            string searchTerm = null,
            PlayerSorting sorting = PlayerSorting.Team,
            int currentPage = 1,
            int playersPerPage = int.MaxValue,
            bool publicOnly = true);

        PlayerDetailsServiceModel Details(Guid playerId);

        Guid Create(
            string firstName,
            string middleName,
            string lastName,
            string team,
            int age,
            double weight,
            double height,
            string image,
            int goal,
            byte shirtNumber,
            string nationality,
            string description,
            Guid positionId,
            Guid managerId);

        bool Edit(
            Guid playerId,
            string firstName,
            string middleName,
            string lastName,
            string team,
            int age,
            double weight,
            double height,
            string image,
            int goal,
            byte shirtNumber,
            string nationality,
            string description,
            Guid positionId,
            bool isPublic);

        Guid Delete(Guid id);

        IEnumerable<PlayerServiceModel> ByUser(string userId);

        bool IsByManager(Guid palyerId, Guid managerId);

        void ChangeVisibility(Guid teamId);

        IEnumerable<string> AllTeams();

        IEnumerable<PlayerPositionServiceModel> AllPositions();

        bool PositionExists(Guid positionId);
    }
}
