namespace Football.Tests.Routing
{
    using Football.Controllers;
    using Football.Core.Models.Stadiums;
    using MyTested.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    public class StadiumsControllerTest
    {
        [Fact]
        public void IndexRouteShouldBeMapped()
              => MyRouting
                  .Configuration()
                  .ShouldMap("/Stadiums/Mine")
                  .To<StadiumsController>(c => c.Mine().ToString());

        [Fact]
        public void PostBecomeShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Stadiums/Mine")
                    .WithMethod(HttpMethod.Post))
                .To<StadiumsController>(c => c.All(With.Any<AllStadiumsQueryModel>()));
    }
}
