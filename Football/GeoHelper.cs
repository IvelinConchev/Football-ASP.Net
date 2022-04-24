namespace Football
{
    public class GeoHelper
    {
        private readonly HttpClient _httpClient;
        public GeoHelper()
        {
            _httpClient = new HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(5)
            };
        }
        private async Task<string> GetIPAddress()
        {
            var ipAddress = await _httpClient.GetAsync($"http://ipinfo.io/ip");
            if (ipAddress.IsSuccessStatusCode)
            {
                var json = await ipAddress.Content.ReadAsStringAsync();
                return json.ToString();
                //return "87.227.250.217";
                //return "45.58.142.16";
                //return "134.201.250.155";
            }
            return "";
        }
        public async Task<string> GetGeoInfo()
        {
            var ipAddress = await GetIPAddress();
            var response = await _httpClient.GetAsync($"http://api.ipstack.com/" + ipAddress + "?access_key=18ad017b24a0f88a6bc02823d50f0c4c");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return json;
            }
            return null;
        }
    }
}
