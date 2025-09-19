using Newtonsoft.Json;

namespace TgMuApp.Model
{
    public class AccItem
    {
        [JsonIgnore]
        [JsonProperty("session_file")]
        public string SessionFile { get; set; }


        [JsonProperty("phone")]
        public string Phone { get; set; }


        [JsonProperty("app_id")]
        public int AppId { get; set; }

        [JsonProperty("app_hash")]
        public string AppHash { get; set; }


        [JsonIgnore]
        [JsonProperty("register_time")]
        public long RegisterTime { get; set; }


        [JsonProperty("password_2fa")]
        public string Password { get; set; }

        [JsonProperty("twoFA")]
        public string TwoFa { get; set; }
    }
}
