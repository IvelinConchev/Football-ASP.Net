namespace Football.Extensions
{
    using Football.Core.Contracts;

    public static class PlayerModelExtensions
    {
        public static string GetInformation(this IPlayerModel player)
            => player.FirstName + "-" + player.LastName + "-" + player.Team;
    }
}
