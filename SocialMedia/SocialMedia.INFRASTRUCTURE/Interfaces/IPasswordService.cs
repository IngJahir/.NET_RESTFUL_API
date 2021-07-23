namespace SocialMedia.INFRASTRUCTURE.Interfaces
{
    public interface IPasswordService
    {
        bool Check(string hash, string password);
        string Hash(string password);        
    }
}
