namespace Football.Core.Services.Cities
{
    using System;
    using System.Collections.Generic;
    using Football.Core.Contracts;
    using Football.Core.Models.Cities;
    using Football.Core.Services.Cities.Models;
    using Football.Infrastructure.Data;
    using Football.Infrastructure.Data.Models;

    public class CityService : ICityService
    {
        private readonly FootballDbContext data;

        public CityService(FootballDbContext _data)
        {
            this.data = _data;
        }

        public CityQueryServiceModel All
            (string name,
            string searchTerm,
            CitySorting sorting,
            int currentPage,
            int citiesPerPage)
        {
            var citiesQuery = this.data.Cities.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                citiesQuery = citiesQuery.Where(c => c.Name == name);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                citiesQuery = citiesQuery.Where(p => (p.Name).ToLower().Contains(searchTerm.ToLower()));
            }

            //TODO or _=>
            citiesQuery = sorting switch
            {
                CitySorting.Name => citiesQuery.OrderBy(p => p.Name),
                CitySorting.PostCode => citiesQuery.OrderBy(p => p.Name),
            };

            var totalCities = citiesQuery.Count();

            var cities = GetCities(citiesQuery
                .Skip((currentPage - 1) * citiesPerPage)
                .Take(citiesPerPage));

            return new CityQueryServiceModel
            {
                TotalCities = totalCities,
                CurrentPage = currentPage,
                CitiesPerPage = citiesPerPage,
                Cities = cities,
            };
        }

        public CityDetailsServiceModel Details(Guid id)
        => this.data
            .Cities
            .Where(c => c.Id == id)
            .Select(c => new CityDetailsServiceModel
            {
                Id = c.Id,
                PostCode = c.PostCode,
                Image = c.Image,
                Description = c.Desctription,
                //TeamName = c.TeamCities.
                //UserId = c.TeamCities
            })
            .FirstOrDefault();

        public Guid Create(string name, string postCode, string image, string description, Guid teamId)
        {
            var cityData = new City
            {
                Name = name,
                PostCode = postCode,
                Image = image,
                Desctription = description,

            };

            this.data.Cities.Add(cityData);
            this.data.SaveChanges();

            return cityData.Id;
        }

        public bool Edit(Guid id, string name, string postCode, string image, string description, Guid teamId)
        {
            var cityData = this.data.Cities.Find(id);
            if (cityData == null)
            {
                return false;
            }

            cityData.Name = name;
            cityData.PostCode = postCode;
            cityData.Image = image;
            cityData.Desctription = description;

            this.data.SaveChanges();

            return true;
        }


        //TODO
        public IEnumerable<CityServiceModel> ByUser(string userId)
        => GetCities(this.data
            .Cities
            .Where(x => x.Name == userId)).ToList();

        //TODO
        public bool IsByManager(Guid cityId, Guid managerId)
        => this.data
            .Teams
            .Any(t => t.Id == cityId && t.Player.ManagerId == managerId);

        private static IEnumerable<CityServiceModel> GetCities
           (IQueryable<City> cityQuery)
           => cityQuery
           .Select(c => new CityServiceModel
           {
               Id = c.Id,
               Name = c.Name,
               PostCode = c.PostCode,
               Image = c.Image,
               Desctription = c.Desctription,
           })
           .ToList();

        public IEnumerable<CityTeamsServiceModel> AllTeams()
            => this.data
            .Teams
            .Select(t => new CityTeamsServiceModel
            {
                Id = t.Id,
                Name = t.Name
            })
            .ToList();

        public bool TeamExists(Guid teamId)
            => this.data
            .Teams
            .Any(t => t.Id == teamId);

        public IEnumerable<string> AllNames()
            => this.data
            .Cities
            .Select(c => c.Name)
            .Distinct()
            .OrderBy(n => n)
            .ToList();
    }
}
