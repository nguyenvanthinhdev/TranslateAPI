using TranslateAPI.Entities;

namespace TranslateAPI.InterFaces
{
    public interface ITranslate
    {
        Task<Translate> TranslateAPI(Account account,TranslateGG translate);
    }
}
