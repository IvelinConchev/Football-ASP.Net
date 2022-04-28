using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football.Tests.Pipeline
{
	public class HomeController
	{
        [Fact]
        public void IndexShouldReturnViewWithCorrectModelAndData()
              => MyMvc
                  .Pipeline()
                  .ShouldMap("/")
                  .To<HomeController>(c => c.Index())
                  .Which(controller => controller
                      .WithData(TenPublicCars))
                  .ShouldReturn()
                  .View(view => view
                      .WithModelOfType<List<LatestCarServiceModel>>()
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
