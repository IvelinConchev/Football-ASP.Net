namespace Football.Core.Services.Players
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Football.Core.Contracts;
    using Football.Core.Models.Players;
    using Football.Infrastructure.Data;
    using Football.Infrastructure.Data.Models;

    public class PlayerService : IPlayerService
    {
        private readonly FootballDbContext data;

        public PlayerService(FootballDbContext _data)
         => this.data = _data;

        public PlayerQueryServiceModel All(
            string team,
            string searchTerm,
            PlayerSorting sorting,
            int currentPage,
            int playersPerPage)
        {
            var playersQuery = this.data.Players.AsQueryable();

            if (!string.IsNullOrWhiteSpace(team))
            {
                playersQuery = playersQuery.Where(c => c.Team == team);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                playersQuery = playersQuery.Where(
                    p => (p.FirstName + " " + p.MiddleName + " " + p.LastName).ToLower().Contains(searchTerm.ToLower())
                    || (p.FirstName + " " + p.LastName).ToLower().Contains(searchTerm.ToLower())
                    || (p.MiddleName + " " + p.LastName).ToLower().Contains(searchTerm.ToLower())
                    || (p.FirstName + " " + p.LastName + " " + p.Team).ToLower().Contains(searchTerm.ToLower())
                    || p.Team.ToLower().Contains(searchTerm.ToLower())
                    || p.Position.Name.ToLower().Contains(searchTerm.ToLower())
                    || p.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            playersQuery = sorting switch
            {
                PlayerSorting.FirstAndMiddleAndLastName => playersQuery.OrderBy(p => p.FirstName)
                .ThenBy(p => p.MiddleName).ThenBy(p => p.LastName),
                PlayerSorting.Team => playersQuery.OrderByDescending(p => p.Team),
                PlayerSorting.Position => playersQuery.OrderBy(p => p.Position),
                PlayerSorting.Description or _ => playersQuery.OrderByDescending(p => p.Id)
                //CarSorting.DateCreated or _ => playerQuery.OrderByDescending(p => p.Id)
            };

            var totalPlayers = playersQuery.Count();

            var players = GetPlayers(playersQuery
                .Skip((currentPage - 1) * playersPerPage)
                .Take(playersPerPage));

            return new PlayerQueryServiceModel
            {
                TotalPlayers = totalPlayers,
                CurrentPage = currentPage,
                PlayersPerPage = playersPerPage,
                Players = players
            };
        }

        public PlayerDetailsServiceModel Details(Guid id)
            => this.data
            .Players
            .Where(p => p.Id == id)
            .Select(p => new PlayerDetailsServiceModel
            {
                Id = p.Id,
                FirstName = p.FirstName,
                MiddleName = p.MiddleName,
                LastName = p.LastName,
                Team = p.Team,
                Age = p.Age,
                Weight = p.Weight,
                Height = p.Height,
                Image = p.Image,
                Goal = p.Goal,
                ShirtNumber = p.ShirtNumber,
                Nationality = p.Nationality,
                Description = p.Description,
                PositionName = p.Position.Name,
                UserId = p.Manager.UserId
            })
            .FirstOrDefault();

        public Guid Create(string firstName, string middleName, string lastName, string team, int age, double weight, double height, string image, int goal, byte shirtNumber, string nationality, string description, Guid positionId, Guid managerId)
        {
            var playerData = new Player
            {
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                Team = team,
                Age = age,
                Weight = weight,
                Height = height,
                Image = image,
                Goal = goal,
                ShirtNumber = shirtNumber,
                Nationality = nationality,
                Description = description,
                PositionId = positionId,
                ManagerId = managerId
            };

            this.data.Players.Add(playerData);
            this.data.SaveChanges();

            return playerData.Id;
        }

        public bool Edit(Guid id, string firstName, string middleName, string lastName, string team, int age, double weight, double height, string image, int goal, byte shirtNumber, string nationality, string description, Guid positionId)
        {
            var playerData = this.data.Players.Find(id);

            if (playerData == null)
            {
                return false;
            }

            playerData.FirstName = firstName;
            playerData.MiddleName = middleName;
            playerData.LastName = lastName;
            playerData.Team = team;
            playerData.Age = age;
            playerData.Weight = weight;
            playerData.Height = height;
            playerData.Image = image;
            playerData.Goal = goal;
            playerData.ShirtNumber = shirtNumber;
            playerData.Nationality = nationality;
            playerData.Description = description;
            playerData.PositionId = positionId;

            this.data.SaveChanges();

            return true;
        }

        public IEnumerable<string> AllPlayerTeams()
            => this.data
            .Players
            .Select(p => p.Team)
            .Distinct()
            .OrderBy(t => t)
            .ToList();

        public IEnumerable<PlayerServiceModel> ByUser(string userId)
            => GetPlayers(this.data
                .Players
                .Where(p => p.Manager.UserId == userId));

        public bool IsByManager(Guid playerId, Guid managerId)
            => this.data
            .Players
            .Any(p => p.Id == managerId && p.ManagerId == managerId);
        
        private static IEnumerable<PlayerServiceModel> GetPlayers(IQueryable<Player> playerQuery)
            => playerQuery
            .Select(p => new PlayerServiceModel
            {
                Id = p.Id,
                FirstName = p.FirstName,
                MiddleName = p.MiddleName,
                LastName = p.LastName,
                Team = p.Team,
                Age = p.Age,
                Weight = p.Weight,
                Height = p.Height,
                Image = p.Image,
                ShirtNumber = p.ShirtNumber,
                Nationality = p.Nationality,
                Description = p.Description,
                PositionName = p.Position.Name
            })
            .ToList();

        public IEnumerable<PlayerPositionServiceModel> AllPositions()
          => this.data
          .Positions
          .Select(p => new PlayerPositionServiceModel
          {
              Id = p.Id,
              Name = p.Name,
          })
          .ToList();


        public bool PositionExists(Guid positionId)
            => this.data
            .Positions
            .Any(p => p.Id == positionId);

        public IEnumerable<string> AllTeams()
            => this.data
            .Players
            .Select(p => p.Team)
            .Distinct()
            .OrderBy(t => t)
            .ToList();

    }
}

