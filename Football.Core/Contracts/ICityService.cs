namespace Football.Core.Contracts
{
    using System;
    using System.Collections.Generic;
    using Football.Core.Models.Cities;
    using Football.Core.Services.Cities.Models;
    public interface ICityService
    {
        CityQueryServiceModel All(
            string team = null,
            string searchTerm = null,
            CitySorting sorting = CitySorting.Name,
            int currentPage = 1,
            int citiesPerPage = int.MaxValue,
            bool publicOnly = true);

        CityDetailsServiceModel Details(Guid guid);

        Guid Create(
            string name,
            string postCode,
            string image,
            string description,
            Guid teamId);

        bool Edit(Guid id,
            string name,
            string postCode,
            string image,
            string description,
            Guid teamId,
            bool isPublic);

        Guid Delete(Guid id);

        IEnumerable<CityServiceModel> ByUser(string userId);

        bool IsByManager(Guid cityId, Guid managerId);

        void ChangeVisibility(Guid teamId);

        IEnumerable<string> AllNames();

        IEnumerable<CityTeamsServiceModel> AllTeams();

        bool TeamExists(Guid teamId);
    }
}
