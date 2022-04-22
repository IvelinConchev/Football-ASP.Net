namespace Football.Core.Contracts
{
    using Football.Core.Models.Stadiums;
    using Football.Core.Services.Stadiums.Models;
    using System;
    using System.Collections.Generic;

    public interface IStadiumService
    {
        StadiumQueryServiceModel All(
            string name = null,
            string searchTerm = null,
            StadiumSorting sorting = StadiumSorting.Name,
            int currentPage = 1,
            int stadiumsPerPage = int.MaxValue,
            bool publicOnly = true);

        StadiumDetailsServiceModel Details(Guid stadiumId);

        Guid Create(
            string name,
            string image,
            string description,
            int capacity,
            string address,
            Guid cityId,
            Guid managerId);

        bool Edit(
            Guid stadiumId,
            string name,
            string image,
            string description,
            int capacity,
            string address,
            Guid cityId,
            bool isPublic);

        Guid Delete(Guid id);

        IEnumerable<StadiumServiceModel> ByUser(string userId);

        bool IsByManager(Guid stadiumId, Guid managerId);

        void ChangeVisibility(Guid teamId);

        IEnumerable<string> AllNames();

        IEnumerable<StadiumCitiesServiceModel> AllCities();

        bool CityExists(Guid cityId);
    }
}
