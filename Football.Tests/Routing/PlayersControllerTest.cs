namespace Football.Tests.Routing
{
    using Football.Controllers;
    using Football.Core.Models.Players;
    using MyTested.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    public class PlayersControllerTest
    {
        [Fact]
        public void IndexRouteShouldBeMapped()
               => MyRouting
                   .Configuration()
                   .ShouldMap("/Players/Mine")
                   .To<PlayersController>(c => c.Mine().ToString());

        [Fact]
        public void PostBecomeShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Players/All")
                    .WithMethod(HttpMethod.Post))
                .To<PlayersController>(c => c.All(With.Any<AllPlayersQueryModel>()));
    }
}
