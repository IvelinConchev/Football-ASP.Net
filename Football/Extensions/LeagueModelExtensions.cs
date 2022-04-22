namespace Football.Extensions
{
    using Football.Core.Contracts;

    public static class LeagueModelExtensions
    {
        public static string GetInformation(this ILeagueModel league)
           => league.Name + "-";
    }
}
