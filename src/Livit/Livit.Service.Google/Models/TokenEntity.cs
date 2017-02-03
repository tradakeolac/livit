namespace Livit.Service.Google.Models
{
    using Newtonsoft.Json;

    public class TokenEntity
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]

        public string ExpiredIn { get; set; }
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }
    }
}
