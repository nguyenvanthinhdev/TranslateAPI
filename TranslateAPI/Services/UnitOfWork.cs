using TranslateAPI.ConText;
using TranslateAPI.InterFaces;

namespace TranslateAPI.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _DbConText;
        public UnitOfWork(AppDbContext DbConText) { _DbConText = DbConText; }
        public IUser user =>
            new UserService(_DbConText);
        public ITranslate translate =>
            new TranslateService(_DbConText);
        public async Task<bool> SaveAsync()
        {
            return await _DbConText.SaveChangesAsync() > 0;
        }
    }
}
