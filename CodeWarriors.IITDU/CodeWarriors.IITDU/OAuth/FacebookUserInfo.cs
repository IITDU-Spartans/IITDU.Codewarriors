using Newtonsoft.Json;

namespace CodeWarriors.IITDU.OAuth
{
    public class FacebookUserInfo : IOAuthUserInfo
    {
        [JsonProperty(PropertyName = "is_valid")]
        public bool Verified { get; set; }

        [JsonProperty(PropertyName = "user_id")]
        public string Id { get; set; }

        public string FullName { get; set; }
        public string ProfileLink { get; set; }
        public string Gender { get; set; }

        [JsonProperty(PropertyName = "expires_at")]
        public string ExpiresAt { get; set; }

        public string UniqueId { get { return "Facebook" + Id; } }

        //public string app_id { get; set; }
        //public string application { get; set; }
        //public string expires_at { get; set; }
        //public string is_valid { get; set; }
        //public string issued_at { get; set; }
        //public string user_id { get; set; }
    }

    public class FBData
    {
        [JsonProperty("data")]
        public FacebookUserInfo FacebookUserInfo { get; set; }
    }
}