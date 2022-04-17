namespace Football.Core.Contracts
{
    using System;
    using Football.Core.Services.Positions;

    public interface IPositionService
    {
        PositionServiceModel Details(Guid positionid);
        Guid Create(
           string Name);
    }
}
