using Newtonsoft.Json;

namespace CodeWarriors.IITDU.OAuth
{
    public class GooglaUserInfo : IOAuthUserInfo
    {
        [JsonProperty("verified_email")]
        public bool Verified { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("given_name")]
        public string GivenName { get; set; }

        [JsonProperty("family_name")]
        public string FamilyName { get; set; }

        [JsonProperty("name")]
        public string FullName { get; set; }

        [JsonProperty("link")]
        public string ProfileLink { get; set; }

        [JsonProperty("picture")]
        public string PhotoUrl { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonIgnore]
        public string UniqueId { get { return Email; } }
    }
}