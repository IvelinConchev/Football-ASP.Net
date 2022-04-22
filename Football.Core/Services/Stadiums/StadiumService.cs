namespace Football.Core.Services.Stadiums
{
    using System;
    using System.Collections.Generic;
    using Football.Core.Contracts;
    using Football.Core.Models.Stadiums;
    using Football.Core.Services.Stadiums.Models;
    using Football.Infrastructure.Data;
    using Football.Infrastructure.Data.Models;

    public class StadiumService : IStadiumService
    {
        private readonly FootballDbContext data;

        public StadiumService(FootballDbContext _data)
        {
            this.data = _data;
        }

        public StadiumQueryServiceModel All(
            string name = null,
            string searchTerm = null,
            StadiumSorting sorting = StadiumSorting.Name,
            int currentPage = 1,
            int stadiumsPerPage = int.MaxValue,
            bool publicOnly = true)
        {
            var stadiumsQuery = this.data.Stadiums
                .Where(c => !publicOnly || c.IsPublic);

            if (!string.IsNullOrWhiteSpace(name))
            {
                stadiumsQuery = stadiumsQuery.Where(s => s.Name == name);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                stadiumsQuery = stadiumsQuery.Where(s => s.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            stadiumsQuery = sorting switch
            {
                StadiumSorting.Name => stadiumsQuery.OrderBy(s => s.Name),
                StadiumSorting or _ => stadiumsQuery.OrderByDescending(s => s.Id)
            };

            var totalStadiims = stadiumsQuery.Count();

            var stadiums = GetStadiums(stadiumsQuery
                .Skip((currentPage - 1) * stadiumsPerPage)
                .Take(stadiumsPerPage));

            return new StadiumQueryServiceModel
            {
                TotalStadiums = totalStadiims,
                CurrentPage = currentPage,
                StadiumsPerPage = stadiumsPerPage,
                Stadiums = stadiums,
            };
        }

        public StadiumDetailsServiceModel Details(Guid id)
        => this.data
            .Stadiums
            .Where(s => s.Id == id)
            .Select(s => new StadiumDetailsServiceModel
            {
                Id = s.Id,
                Name = s.Name,
                Image = s.Image,
                Description = s.Description,
                Capacity = s.Capacity,
                Address = s.Address,
                //CityName = s.City.Name
            })
            .FirstOrDefault();


        public Guid Create(
            string name,
            string image,
            string description,
            int capacity,
            string address,
            Guid cityId,
            Guid managerId)
        {
            var stadiumData = new Stadium
            {
                Name = name,
                Image = image,
                Description = description,
                Capacity = capacity,
                Address = address,
                IsPublic = false,
            };

            this.data.Stadiums.Add(stadiumData);

            this.data.SaveChanges();

            return stadiumData.Id;
        }

        public bool Edit(
            Guid id,
            string name,
            string image,
            string description,
            int capacity,
            string address,
            Guid cityId,
            bool isPublic)
        {
            var stadiumData = this.data.Stadiums.Find(id);

            if (stadiumData == null)
            {
                return false;
            }

            stadiumData.Name = name;
            stadiumData.Image = image;
            stadiumData.Description = description;
            stadiumData.Capacity = capacity;
            stadiumData.Address = address;
            stadiumData.IsPublic = isPublic;

            this.data.SaveChanges();

            return true;
        }

        public IEnumerable<StadiumServiceModel> ByUser(string userId)
        => GetStadiums(this.data
            .Stadiums
            .Where(s => s.Name == userId));

        public bool IsByManager(Guid stadiumId, Guid managerId)
        => this.data
            .Stadiums
            .Any(s => s.Id == stadiumId);

        public IEnumerable<string> AllNames()
        => this.data
            .Stadiums
            .Select(s => s.Name)
            .Distinct()
            .OrderBy(n => n)
            .ToList();

        public IEnumerable<StadiumCitiesServiceModel> AllCities()
        => this.data
            .Cities
            .Select(c => new StadiumCitiesServiceModel
            {
                Id = c.Id,
                Name = c.Name,
            })
            .ToList();


        public bool CityExists(Guid cityId)
        => this.data
            .Cities
            .Any(c => c.Id == cityId);

        public bool ManagerExists(Guid managerId)
             => this.data
             .Managers
             .Any(m => m.Id == managerId);

        private static IEnumerable<StadiumServiceModel>
            GetStadiums(IQueryable<Stadium> stadiumQuery)
            => stadiumQuery
            .Select(s => new StadiumServiceModel
            {
                Id = s.Id,
                Name = s.Name,
                Image = s.Image,
                Description = s.Description,
                Capacity = s.Capacity,
                IsPublic = s.IsPublic
                //CityName = s.City
            })
            .ToList();

        public void ChangeVisibility(Guid stadiumId)
        {
            var stadium = this.data.Stadiums.Find(stadiumId);

            stadium.IsPublic = !stadium.IsPublic;

            this.data.SaveChanges();
        }

        public Guid Delete(Guid id)
        {
            var deleteStadium = this.data
                .Stadiums
                .FirstOrDefault(t => t.Id == id);

            var result = data.Stadiums.Remove(deleteStadium);

            this.data.SaveChanges();

            return id;
        }
    }
}
