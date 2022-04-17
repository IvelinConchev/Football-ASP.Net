﻿namespace Football.Core.Contracts
{
    using System;
    using System.Collections.Generic;
    using Football.Core.Models.Cities;
    using Football.Core.Services.Cities;

    public interface ICityService
    {
        CityQueryServiceModel All(
            string team,
            string searchTerm,
            CitySorting sorting,
            int currentPage,
            int citiesPerPage);

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
            Guid teamId);

        IEnumerable<CityServiceModel> ByUser(string userId);    

        bool IsByManager(Guid cityId, Guid managerId);

        IEnumerable<string> AllTeams();

        IEnumerable<CityTeamsServiceModel> AllCities();

        bool TeamExists(Guid teamId);
    }
}