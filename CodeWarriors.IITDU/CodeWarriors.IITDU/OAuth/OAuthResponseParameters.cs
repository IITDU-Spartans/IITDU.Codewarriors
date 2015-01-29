using Newtonsoft.Json;

namespace CodeWarriors.IITDU.OAuth
{
    public class OAuthResponseParameters
    {
        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

        public bool HasError { get { return !string.IsNullOrWhiteSpace(Error); } }
    }
}
