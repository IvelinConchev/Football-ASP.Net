namespace Football.Tests.Mocks
{
    using Football.Infrastructure.Data;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class DatabaseMock
    {
        public static FootballDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<FootballDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    //.UseInMemoryDatabase("Footbal")
                    .Options;

                return new FootballDbContext(dbContextOptions);
            }
        }
    }
}
