using FluentAssertions;
using Football.Controllers;
using Football.Core.Services.Teams.Models;
using MyTested.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;

using static Football.Tests.Data.Teams;

namespace Football.Tests.Pipeline
{
    public class HomeControllerTest
	{
        [Fact]
        public void IndexShouldReturnViewWithCorrectModelAndData()
              => MyMvc
                  .Pipeline()
                  .ShouldMap("/")
                  .To<HomeController>(c => c.Index())
                  .Which(controller => controller
                      .WithData(TenPublicTeams))
                  .ShouldReturn()
                  .View(view => view
                      .WithModelOfType<List<LatestTeamServiceModel>>()
                      .Passing(m => m.Should().HaveCount(3)));


        [Fact]
        public void ErrorShouldReturnView()
            => MyMvc
                .Pipeline()
                .ShouldMap("/Home/Error")
                .To<HomeController>(c => c.Error())
                .Which()
                .ShouldReturn()
                .View();
    }
}
