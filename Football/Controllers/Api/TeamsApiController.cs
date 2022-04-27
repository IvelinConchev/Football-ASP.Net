namespace Football.Controllers.Api
{
    using Football.Core.Contracts;
    using Football.Core.Services.Teams.Models;
    using Football.Models.Api.Teams;
    using Microsoft.AspNetCore.Mvc;

    public class TeamsApiController : ControllerBase
    {
        private readonly ITeamService teams;

        public TeamsApiController(ITeamService _teams)
            => this.teams = _teams;

        [HttpGet]

        public TeamQueryServiceModel All([FromQuery]
            AllTeamsApiRequestModel query)
            => this.teams.All(
                query.Name,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                query.TeamsPerPage);
    }
}
