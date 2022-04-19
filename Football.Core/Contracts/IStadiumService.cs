namespace Football.Core.Contracts
{
    using Football.Core.Models.Stadiums;
    using Football.Core.Services.Stadiums;
    using Football.Core.Services.Stadiums.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IStadiumService
    {
        StadiumQueryServiceModel All(
            string name,
            string searchTerm,
            StadiumSorting sorting,
            int currentPage,
            int stadiumsPerPage);

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
            Guid cityId);

        IEnumerable<StadiumServiceModel> ByUser(string userId);

        bool IsByManager(Guid stadiumId, Guid managerId);

        IEnumerable<string> AllNames();

        IEnumerable<StadiumCitiesServiceModel> AllCities();

        bool CityExists(Guid cityId);
    }
}
