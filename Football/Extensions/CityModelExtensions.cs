namespace Football.Extensions
{
    using Football.Core.Contracts;

    public static class CityModelExtensions
    {
        public static string GetInformation(this ICityModel city)
           => city.Name + "-" + city.PostCode;
    }
}
