namespace Football.Tests.Routing
{
    using Football.Controllers;
    using Football.Core.Models.Positions;
    using MyTested.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    public class PositionsControllerTest
    {
        [Fact]
        public void GetPositionShouldBeMapped()
              => MyRouting
                  .Configuration()
                  .ShouldMap("/Positions/Index")
                  .To<PositionsController>(c => c.Index());

    }
}
