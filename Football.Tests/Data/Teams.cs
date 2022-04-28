namespace Football.Tests.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using Football.Infrastructure.Data.Models;
    public static class Teams
    {
        public static IEnumerable<Team> TenPublicTeams
           => Enumerable.Range(0, 10).Select(i => new Team
           {
               IsPublic = true
           });
    }
}
