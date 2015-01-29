namespace CodeWarriors.IITDU.OAuth
{
    public interface IOAuthTokenResponse
    {
        string AccessToken { get; }
        string IdToken { get; }
        string ExpiresIn { get;}
        string TokenType { get;}
        string RefreshToken { get; }
    }
}
