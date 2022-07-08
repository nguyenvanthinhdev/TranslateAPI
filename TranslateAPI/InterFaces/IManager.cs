using TranslateAPI.Entities;
using TranslateAPIgoogle.Helper;

namespace TranslateAPI.InterFaces
{
    public interface IManager
    {
        Task<Manager> Get_Infor_Manager();
        IQueryable<Translate> Get_Order(int? userID);
        Task<List<Translate>> Get(SystemManager systemManager);
        Task<string> UnOrBlock(UnlockOrBock? UnlockOrBock = null, int? UnOrBlock = null);
        Task<string> Add_Coin(string name,int coin);
        Task<string> Minus_Coin(string name,int coin);
    }
}
