using TranslateAPI.Entities;

namespace TranslateAPI.InterFaces
{
    public interface IUser
    {
        Task<string> CreateUser(Account account,string password);
        Task<string> Login(Account account);
        Task<string> ChangerPassword(Account account, string PassWork);
        Task<List<User>> GetUser(int? UserID = null, string? Name = null);
    }
}
