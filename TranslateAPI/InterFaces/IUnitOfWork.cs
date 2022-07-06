namespace TranslateAPI.InterFaces
{
    public interface IUnitOfWork
    {
        IUser user { get; }
        ITranslate translate { get; }
        IManager manager { get; }
        Task<bool> SaveAsync();
    }
}
