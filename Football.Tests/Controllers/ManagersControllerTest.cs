namespace Football.Tests.Controllers
{
    using System.Linq;
    using Football.Controllers;
    using Football.Core.Models.Teams;
    using Football.Infrastructure.Data.Models;
    using Football.Models.Managers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using static WebConstants;
    public class ManagersControllerTest
    {


        [Fact]
        public void GetBecomeShouldBeForAuthorizedUsersAndReturnView()
                   => MyController<ManagersController>
                       .Instance()
                       .Calling(c => c.Become())
                       .ShouldHave()
                       .ActionAttributes(attributes => attributes
                           .RestrictingForAuthorizedRequests())
                       .AndAlso()
                       .ShouldReturn()
                       .View();

        [Theory]
        [InlineData("Manager", "+359888888888")]
        public void PostBecomeShouldBeForAuthorizedUsersAndReturnRedirectWithValidModel(
            string managerName,
            string phoneNumber)
            => MyController<ManagersController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(c => c.Become(new BecomeManagerFormModel
                {
                    Name = managerName,
                    PhoneNumber = phoneNumber
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .Data(data => data
                    .WithSet<Manager>(managers => managers
                        .Any(m =>
                            m.Name == managerName &&
                            m.PhoneNumber == phoneNumber &&
                            m.UserId == TestUser.Identifier)))
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(GlobalMessageKey))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<TeamsController>(c => c.All(With.Any<AllTeamQueryModel>())));
    }
}

