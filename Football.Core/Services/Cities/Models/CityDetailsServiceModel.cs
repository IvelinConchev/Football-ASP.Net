namespace Football.Core.Services.Cities.Models
{
    using System;

    public class CityDetailsServiceModel : CityServiceModel
    {
        public string Description { get; init; }

        public Guid TeamId { get; init; }

        public Guid ManagerId { get; init; }

        public string ManagerName { get; init; }

        public string UserId { get; init; }
    }
}
