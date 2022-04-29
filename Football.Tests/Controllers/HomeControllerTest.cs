namespace Football.Tests.Controllers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Football.Controllers;
    using Football.Core.Services.Statistics;
    using Football.Core.Services.Teams;
    using Football.Infrastructure.Data.Identity;
    using Football.Infrastructure.Data.Models;
    using Football.Models.Home;
    using Football.Tests.Mock;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using Moq;
    using Xunit;
    using FluentAssertions;
    using MyTested.AspNetCore.Mvc;
    using Football.Core.Services.Teams.Models;

    using static WebConstants.Cache;
    using static Data.Teams;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnViewWithCorrectModel()
        {
            //Arrange
            var data = DatabaseMock.Instance;

            var teams = Enumerable
                .Range(0, 10)
                .Select(i => new Team());

            data.Teams.AddRange(teams);
            data.Users.Add(new ApplicationUser());
            data.SaveChanges();

            var teamService = new TeamService(data);
            var cache = new Mock<IMemoryCache>();
            var statisticsServise = new StatisticsService(data);

            var homeController = new HomeController(teamService, statisticsServise, cache.Object);

            //Act
            var result = homeController.Index();



            //Assert

            result
               .Should()
               .NotBeNull()
               .And
               .BeAssignableTo<ViewResult>()
               .Which
               .Model
               .Should()
               .As<IndexViewModel>()
              .Invoking(model =>
              {
                  model.Teams.Should().HaveCount(3);
                  model.TotalUsers.Should().Be(1);
              })
              .Invoke();
            //Assert.NotNull(result);
            //var viewResult = Assert.IsType<ViewResult>(result);

            //var model = viewResult.Model;

            //var indexViewModel = Assert.IsType<IndexViewModel>(model);

            //Assert.Equal(3, indexViewModel.Teams.Count);
            //Assert.Equal(10, indexViewModel.TotalTeams);
            //Assert.Equal(2, indexViewModel.TotalUsers);
        }

        [Fact]
        public void ErrorShouldReturnView()
        {
            //Arrange
            var homeController = new HomeController(null, null, null);

            //Act
            var result = homeController.Error();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void IndexShouldReturnViewWithCorrectTeamAndData()
            => MyMvc
                .Pipeline()
                .ShouldMap("/")
                .To<HomeController>(c => c.Index())
                .Which(controller => controller
                    .WithData(GetTeams()))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<IndexViewModel>()
                    .Passing(m => m.Teams.Should().HaveCount(3)));

        [Fact]
        public void ErrorShouldReturnViews()
            => MyMvc
                .Pipeline()
                .ShouldMap("/Home/Error")
                .To<HomeController>(c => c.Error())
                .Which()
                .ShouldReturn()
                .View();

    private static IEnumerable<Team> GetTeams()
            => Enumerable.Range(0, 10).Select(i => new Team());

        [Fact]
        public void IndexShouldReturnCorrectViewWithName()
           => MyController<HomeController>
               .Instance(controller => controller
                   .WithData(TenPublicTeams))
               .Calling(c => c.Index())
               .ShouldHave()
               .MemoryCache(cache => cache
                   .ContainingEntry(entry => entry
                       .WithKey(LatestTeamsCacheKey)
                       .WithAbsoluteExpirationRelativeToNow(TimeSpan.FromMinutes(15))
                       .WithValueOfType<List<LatestTeamServiceModel>>()))
               .AndAlso()
               .ShouldReturn()
               .View(view => view
                   .WithModelOfType<List<LatestTeamServiceModel>>()
                   .Passing(model => model.Should().HaveCount(3)));
    }
}
