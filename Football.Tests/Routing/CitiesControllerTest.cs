namespace Football.Tests.Routing
{
    using Football.Controllers;
    using Football.Core.Models.Cities;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class CitiesControllerTest
    {
        [Fact]
        public void IndexRouteShouldBeMapped()
                 => MyRouting
                     .Configuration()
                     .ShouldMap("/Cities/Mine")
                     .To<CitiesController>(c => c.Mine().ToString());

        [Fact]
        public void PostBecomeShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Cities/All")
                    .WithMethod(HttpMethod.Post))
                .To<CitiesController>(c => c.All(With.Any<AllCityQueryModel>()));
    }
}
