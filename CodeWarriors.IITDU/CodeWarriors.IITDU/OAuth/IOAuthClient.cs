namespace CodeWarriors.IITDU.OAuth
{
    public interface IOAuthClient
    {
        string GetSignInUrl(string state, string redirectUri);
        IOAuthTokenResponse GetTokenResponse(string code, string redirectUri);
        IOAuthUserInfo GetUserInfo(string accessToken);
    }
}
