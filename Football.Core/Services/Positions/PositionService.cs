namespace Football.Core.Services.Positions
{
    using Football.Core.Contracts;
    using Football.Core.Services.Positions.Models;
    using Football.Infrastructure.Data;
    using Football.Infrastructure.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PositionService : IPositionService
    {
        private readonly FootballDbContext data;

        public PositionService(FootballDbContext _data)
        {
            this.data = _data;
        }

        public Guid Create(string Name)
        {
            var positionData = new Position
            {
                Name = Name,
            };

            this.data.Positions.Add(positionData);  

            this.data.SaveChanges();

            return positionData.Id;
        }

        public PositionServiceModel Details(Guid positionid)
        => this.data
            .Positions
            .Where(p => p.Id == positionid)
            .Select(p => new PositionServiceModel
            {
                Id = p.Id,
                Name = p.Name,
            })
            .FirstOrDefault();
    }
}
