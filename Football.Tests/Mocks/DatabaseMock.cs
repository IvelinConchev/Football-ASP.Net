namespace Football.Tests.Mock
{
    using Football.Infrastructure.Data;
    using Microsoft.EntityFrameworkCore;
    using System;

    public static class DatabaseMock
    {
        public static FootballDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<FootballDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new FootballDbContext(dbContextOptions);
            }
        }
    }
}
