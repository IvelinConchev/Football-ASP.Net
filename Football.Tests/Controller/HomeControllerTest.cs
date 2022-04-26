namespace Football.Tests.Controller
{
    using Football.Controllers;
    //using Football.Controllers;
    using Football.Core.Services.Statistics;
    using Football.Core.Services.Teams;
    using Football.Infrastructure.Data.Models;
    using Football.Models.Home;
    using Football.Tests.Mocks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnViewWithCorrectModel()
        {
            var data = DatabaseMock.Instance;
            //Arrange
            var teamService = new TeamService(data);
            var statisticsService = new StatisticsService(data);
            //var cache = new MemoryCache(List<LatestTeamServiceModel>> (latestTeamCacheKey);

            data.Teams.Add(new Team { Address = "p", AwayKit = "t", Champion = 1, Cup = 1, Defeats = 12, Description = "2412134", HeadCoach = "rqr", HomeKit = "fewfef", Image = "dffdfd", Name = "efwfw", NickName = "ffdfd", LogoUrl = "jklfkd", WebSite = "", Win = 4 });


            //data.Teams.AddRange(Enumerable.Range(0, 10).Select(i => new Team()));
            data.SaveChanges();

            var homeController = new HomeController(teamService, statisticsService, null);

            //Act
            var result = homeController.Index();

            //Assert
            Assert.Null(result);
            var viewResult = Assert.IsType<ViewResult>(result);

            var model = viewResult.Model;

            var indexViewModel = Assert.IsType<IndexViewModel>(model);

            Assert.Equal(3, indexViewModel.Teams.Count);
            Assert.Equal(10, indexViewModel.TotalTeams);
            Assert.Equal(1, indexViewModel.TotalUsers);
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

        public void ShouldTotalStatisticsIndex()
        {
            //Arrange

            //var homeController = new HomeController()
        }
    }
}
