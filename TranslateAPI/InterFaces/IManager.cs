using TranslateAPI.Entities;

namespace TranslateAPI.InterFaces
{
    public interface IManager
    {
        Task<List<Translate>> Get(SystemManager systemManager);
        Task<string> UnOrBlock(UnlockOrBock? UnlockOrBock = null, int? UnOrBlock = null);
    }
}
