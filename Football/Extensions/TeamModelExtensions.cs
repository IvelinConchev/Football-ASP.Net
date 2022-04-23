namespace Football.Extensions
{
    using Football.Core.Contracts;

    public static class TeamModelExtensions
    {
        public static string GetInformation(this ITeamModel team)
            => team.Name + "-" + team.NickName + "-" + team.Champion;
    }
}
