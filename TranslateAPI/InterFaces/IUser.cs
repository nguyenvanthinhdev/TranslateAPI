using TranslateAPI.Entities;

namespace TranslateAPI.InterFaces
{
    public interface IUser
    {
        Task<string> CreateUser(Account account,string password);
        Task<IQueryable<User>> Login(Account account);
        Task<string> ChangerPassword(Account account, string PassWork);
    }
}
