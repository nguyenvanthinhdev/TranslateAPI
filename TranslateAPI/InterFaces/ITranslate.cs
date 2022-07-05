using TranslateAPI.Entities;

namespace TranslateAPI.InterFaces
{
    public interface ITranslate
    {
        Task<Translate> Translate(Account account,TranslateGG translate);
    }
}
