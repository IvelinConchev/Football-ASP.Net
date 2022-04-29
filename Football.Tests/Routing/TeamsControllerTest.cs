namespace Football.Tests.Routing
{
    using Football.Controllers;
    using Football.Core.Models.Teams;
    using MyTested.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    public class TeamsControllerTest
    {
        [Fact]
        public void IndexMineShouldBeMapped()
              => MyRouting
                  .Configuration()
                  .ShouldMap("/Teams/Mine")
                  .To<TeamsController>(c => c.Mine().ToString());

        [Fact]
        public void PostMineShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Teams/Mine")
                    .WithMethod(HttpMethod.Post))
                .To<TeamsController>(c => c.All(With.Any<AllTeamQueryModel>()));
    }
}
