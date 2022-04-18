namespace Football.Core.Services.Leagues
{
    using System;
    using System.Collections.Generic;
    using Football.Core.Contracts;
    using Football.Core.Models.Leagues;
    using Football.Infrastructure.Data;
    using Football.Infrastructure.Data.Models;

    public class LeagueService : ILeagueService
    {
        private readonly FootballDbContext data;

        public LeagueService(FootballDbContext _data)
        {
            this.data = _data;
        }

        public LeagueQueryServiceModel All(
            string name,
            string searchTerm,
            LeagueSorting sorting,
            int currentPage,
            int leaguesPerPage)
        {
            var leaguesQuery = this.data.Leagues.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                leaguesQuery = leaguesQuery.Where(l => l.Name == name);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                leaguesQuery = leaguesQuery.Where(l => l.Name == name);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                leaguesQuery = leaguesQuery.Where(l => l.Name.ToLower().Contains(searchTerm.ToLower()));
                //|| l.Address.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            leaguesQuery = sorting switch
            {
                LeagueSorting.Name => leaguesQuery.OrderBy(l => l.Name),
                _ => leaguesQuery.OrderByDescending(l => l.Id)
            };

            var totalLeagues = leaguesQuery.Count();

            var leagues = GetLeagues(leaguesQuery
                .Skip((currentPage - 1) * leaguesPerPage)
                .Take(leaguesPerPage));

            return new LeagueQueryServiceModel
            {
                TotalLeagues = totalLeagues,
                CurrentPage = currentPage,
                LeaguesPerPage = leaguesPerPage,
                Leagues = leagues
            };
        }

        public LeagueDetailsServiceModel Details(Guid id)
            => this.data
            .Leagues
            .Where(l => l.Id == id)
            .Select(l => new LeagueDetailsServiceModel
            {
                Id = l.Id,
                Name = l.Name,
                Image = l.Image,
                Description = l.Description,
                TeamId = l.TeamId,
                TeamName = l.Team.Name,
                //ManagerId = l,
                //MangerName = l.Team.
            })
            .FirstOrDefault();

        public Guid Create(string name, string image, string description, Guid teamId, Guid managerId)
        {
            var leagueData = new League
            {
                Name = name,
                Image = image,
                Description = description,
                TeamId = teamId,
                // ManagerId = managerId
            };

            this.data.Leagues.Add(leagueData);

            this.data.SaveChanges();

            return leagueData.Id;
        }

        public bool Edit(Guid id, string name, string image, string description, Guid teamId)
        {
            var leagueData = this.data.Leagues.Find(id);

            if (leagueData == null)
            {
                return false;
            }

            leagueData.Name = name;
            leagueData.Image = image;
            leagueData.Description = description;
            leagueData.TeamId = teamId;

            this.data.SaveChanges();

            return true;
        }

        public IEnumerable<string> AllNames()
       => this.data
            .Leagues
            .Select(l => l.Name)
            .Distinct()
            .OrderBy(n => n)
            .ToList();

        public IEnumerable<LeagueServiceModel> ByUser(string userId)
        => GetLeagues(this.data
            .Leagues
            .Where(l => l.Name == userId));

        public bool IsByManager(Guid leagueId, Guid managerId)
        => this.data
            .Leagues
            .Any(l => l.Id == leagueId); //&& l.ManagerId == managerId);

        private static IEnumerable<LeagueServiceModel> GetLeagues(IQueryable<League> leagueQuery)
            => leagueQuery
            .Select(l => new LeagueServiceModel
            {
                Id = l.Id,
                Name = l.Name,
                Image = l.Image,
                Description = l.Description,
            })
            .ToList();

        public IEnumerable<LeagueTeamServiceModel> AllTeams()
        => this.data
            .Teams
            .Select(t => new LeagueTeamServiceModel
            {
                Id = t.Id,
                Name = t.Name,
            })
            .ToList();


        public bool TeamExists(Guid leagueId)
        => this.data
            .Leagues
            .Any(l => l.Id == leagueId);

    }
}
