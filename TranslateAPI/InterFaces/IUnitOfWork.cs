namespace TranslateAPI.InterFaces
{
    public interface IUnitOfWork
    {
        IUser user { get; }
        ITranslate translate { get; }
        Task<bool> SaveAsync();
    }
}
