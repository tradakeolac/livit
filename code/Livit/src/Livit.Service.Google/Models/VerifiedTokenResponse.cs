using Newtonsoft.Json;

namespace Livit.Service.Google.Models
{
    public class VerifiedTokenResponse
    {
        [JsonProperty("sub")]
        public string UserId { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}