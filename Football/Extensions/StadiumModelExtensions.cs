namespace Football.Extensions
{
    using Football.Core.Contracts;

    public static class StadiumModelExtensions
    {
        public static string GetInformation(this IStadiumModel stadium)
           => stadium.Name + "-" + stadium.Capacity;
    }
}
