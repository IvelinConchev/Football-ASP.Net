namespace Football.Tests.Routing
{
    using Football.Controllers;
    using Football.Models.Managers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class ManagersControllerTest
    {
        [Fact]
        public void GetBecomeShouldBeMapped()
               => MyRouting
                   .Configuration()
                   .ShouldMap("/Managers/Become")
                   .To<ManagersController>(c => c.Become());

        [Fact]
        public void PostBecomeShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Managers/Become")
                    .WithMethod(HttpMethod.Post))
                .To<ManagersController>(c => c.Become(With.Any<BecomeManagerFormModel>()));
    }
}
