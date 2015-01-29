namespace CodeWarriors.IITDU.OAuth
{
    public interface IOAuthUserInfo
    {
        bool Verified { get; set; }

        string Id { get; set; }

        string FullName { get; set; }

        string ProfileLink { get; set; }

        string Gender { get; set; }

        string UniqueId { get; }
    }
}