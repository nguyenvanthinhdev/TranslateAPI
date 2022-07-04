using TranslateAPI.Entities;

namespace TranslateAPI.InterFaces
{
    public interface IUser
    {
        Task<string> CreateUser(string Name, string Password);
        Task<List<User>> GetUser(int? UserID = null, string? Name = null);
    }
}
