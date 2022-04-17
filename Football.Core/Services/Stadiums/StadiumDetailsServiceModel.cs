namespace Football.Core.Services.Stadiums
{
    using System;

    public class StadiumDetailsServiceModel : StadiumServiceModel
    {
        public string Description { get; init; }

        public Guid CityId { get; init; }

        public Guid ManagerId { get; init; }

        public string ManagerName { get; init; }

        public string UserId { get; init; }
    }
}
