namespace Football.Models.GeoLocation
{
    using Newtonsoft.Json;

    public class GeoLocationViewModel
    {
        [JsonProperty("ip")]
        public string Ip { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("continent_code")]
        public string ContinentCode { get; set; }

        [JsonProperty("continent_name")]
        public string ContinentName { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("country_name")]

        public string CountryName { get; set; }


        [JsonProperty("region_code")]

        public string RegionCode { get; set; }


        [JsonProperty("region_name")]

        public string RegionName { get; set; }


        [JsonProperty("city")]

        public string City { get; set; }


        [JsonProperty("latitude")]

        public decimal Latitude { get; set; }


        [JsonProperty("longitude")]

        public string Longitude { get; set; }

        //[JsonProperty("location")]
        //public string Location { get; set; }

        [JsonProperty("capital")]
        public string Capital { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }

        //[JsonProperty("name")]
        //public string Name { get; set; }

        //[JsonProperty("country_flag")]
        //public string CountryFlag { get; set; }

        [JsonProperty("country_flag_emoji")]
        public string CountryFlagEmoji { get; set; }

        [JsonProperty("calling_code")]
        public string CallingCode { get; set; }
    }
}

