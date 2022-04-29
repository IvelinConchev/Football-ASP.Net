namespace Football.Tests.Routing
{
    using Football.Controllers;
    using Football.Core.Models.Leagues;
    using MyTested.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    public class LeaguesControllerTest
    {
        [Fact]
        public void IndexRouteShouldBeMapped()
                => MyRouting
                    .Configuration()
                    .ShouldMap("/Leagues/Mine")
                    .To<LeaguesController>(c => c.Mine().ToString());

        [Fact]
        public void PostBecomeShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Leagues/All")
                    .WithMethod(HttpMethod.Post))
                .To<LeaguesController>(c => c.All(With.Any<AllLeaguesQueryModel>()));
    }
}
