using Microsoft.EntityFrameworkCore;
using TranslateAPI.ConText;
using TranslateAPI.Entities;
using TranslateAPI.InterFaces;

namespace TranslateAPI.Services
{
    public class ManagerService:IManager
    {
        private readonly AppDbContext _DbConText;
        public ManagerService(AppDbContext dbContext) { _DbConText = dbContext; }

        private string UpdateUser(int? id = null,string? name = null, int? acctive=null)
        {
            User user = null;
            string res = "";
            if (id.HasValue) { user = _DbConText.Users.Find(id); }
            if (!string.IsNullOrEmpty(name)) { user = _DbConText.Users.FirstOrDefault(x => x.UserName.Contains(name)); }
            if (user != null) 
            {
                res += user.UserID.ToString()+" " + user.UserName;
                user.Active = acctive;
                _DbConText.Users.Update(user);
                _DbConText.SaveChanges();
            }
            if (string.IsNullOrEmpty(res)) { return " không tìm thấy User"; }
            return $"đã update {res}";
        }
        private string UpdateAddressIP(int? id=null, string? name=null,int? acctive = null)
        {
            Address address = null;
            string res = "";
            if (id.HasValue) { address = _DbConText.Addresses.Find(id); }
            if (!string.IsNullOrEmpty(name)) { address = _DbConText.Addresses.FirstOrDefault(x => x.AddressIP.Contains(name)); }
            if (address != null)
            {
                res += address.AddressID.ToString()+" " + address.AddressIP;
                address.Active = acctive;
                _DbConText.Addresses.Update(address);
                _DbConText.SaveChanges();
            }
            if (string.IsNullOrEmpty(res)) { return " không tìm thấy IP nào"; }
            return $"đã update {res}";
        }

        public async Task<List<Translate>> Get(SystemManager systemManager)
        {
            var res = _DbConText.Translates.Include(x=>x.User).ThenInclude(x=>x.Address).ToList();
            if (systemManager.AddressID.HasValue) { res = res.Where(x => x.User.AddressID == systemManager.AddressID).ToList(); }
            if (!string.IsNullOrEmpty(systemManager.Address)) { res = res.Where(x => x.User.Address.AddressIP.Contains(systemManager.Address)).ToList(); }
            if (systemManager.UserID.HasValue) { res = res.Where(x => x.UserID == systemManager.UserID).ToList(); }
            if (!string.IsNullOrEmpty(systemManager.UserName)) { res = res.Where(x => x.User.UserName.Contains(systemManager.UserName)).ToList(); }
            if (!string.IsNullOrEmpty(systemManager.TranslateCode)) { res = res.Where(x => x.TranslateCode.Contains(systemManager.TranslateCode)).ToList(); }
            if (systemManager.TimeFrom.HasValue) { res = res.Where(x => x.TimeTranslates >= systemManager.TimeFrom).ToList(); }
            if (systemManager.TimeTo.HasValue) { res = res.Where(x => x.TimeTranslates <= systemManager.TimeTo).ToList(); }
            return res;
        }

        public async Task<string> UnOrBlock(UnlockOrBock? UnlockOrBock=null, int? acctive = null)
        {
            string res = UpdateUser(UnlockOrBock.UserID, UnlockOrBock.UserName, acctive);
            res += UpdateAddressIP(UnlockOrBock.AddressID, UnlockOrBock.Address, acctive);
            return res;
        }
    }
}
