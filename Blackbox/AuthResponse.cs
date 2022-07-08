using Newtonsoft.Json;

namespace BlaggoBlackbox
{
    public class AuthResponse
    {
        [JsonProperty("data")]
        public Data Data { get; set; }
    }

    public class Tokens
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }

    public class Data
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("tokens")]
        public Tokens Tokens { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("first_login")]
        public bool FirstLogin { get; set; }

        [JsonProperty("tfa")]
        public bool Tfa { get; set; }

        [JsonProperty("is_mobile_allowed")]
        public bool IsMobileAllowed { get; set; }

        [JsonProperty("is_portal_allowed")]
        public bool IsPortalAllowed { get; set; }
    }
}