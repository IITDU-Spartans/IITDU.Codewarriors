using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace CodeWarriors.IITDU.OAuth
{
    public class GoogleOAuthClient : IOAuthClient
    {
        private readonly string _clientId;
        private readonly string _clientSecret;

        public GoogleOAuthClient(string clientId, string clientSecret)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        public string GetSignInUrl(string state, string redirectUri)
        {
            state = state == null ? "" : state.Replace("#", "%23");

            var url =
                string.Format("https://accounts.google.com/o/oauth2/auth?" +
                              "client_id={0}" +
                              "&response_type=code" +
                              "&access_type=online" +
                              "&scope=openid%20email%20profile" +
                              "&redirect_uri={1}" +
                              "&state={2}"
                , _clientId, redirectUri, state);

            return url;
        }

        public IOAuthTokenResponse GetTokenResponse(string code, string redirectUri)
        {
            var postData = HttpUtility.ParseQueryString(String.Empty);
            postData.Add("code", code);
            postData.Add("client_id", _clientId);
            postData.Add("client_secret", _clientSecret);
            postData.Add("redirect_uri", redirectUri);
            postData.Add("grant_type", "authorization_code");

            var postBytes = Encoding.ASCII.GetBytes(postData.ToString());

            var request = WebRequest.Create("https://www.googleapis.com/oauth2/v3/token");
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";

            request.GetRequestStream().Write(postBytes, 0, postBytes.Length);

            using (var reader = new StreamReader(request.GetResponse().GetResponseStream()))
            {
                var responseText = reader.ReadToEnd();

                var tokenResponse = JsonConvert.DeserializeObject<GoogleOAuthTokenResponse>(responseText);
                return tokenResponse;
            }
        }

        public IOAuthUserInfo GetUserInfo(string accessToken)
        {
            var idParseRequest =
                    WebRequest.Create("https://www.googleapis.com/oauth2/v1/userinfo?alt=json&access_token=" + accessToken);

            using (var reader = new StreamReader(idParseRequest.GetResponse().GetResponseStream()))
            {
                var data = reader.ReadToEnd();

                var userInfo = JsonConvert.DeserializeObject<GooglaUserInfo>(data);
                return userInfo;
            }
        }
    }
}