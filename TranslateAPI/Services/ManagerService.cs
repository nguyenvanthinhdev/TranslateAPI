using Microsoft.EntityFrameworkCore;
using TranslateAPI.ConText;
using TranslateAPI.Entities;
using TranslateAPI.InterFaces;
using TranslateAPIgoogle.Helper;

namespace TranslateAPI.Services
{
    public class ManagerService : IManager
    {
        private readonly AppDbContext _DbConText;
        public ManagerService(AppDbContext dbContext) { _DbConText = dbContext; }
        #region private
        private string UpdateUser(int? id = null, string? name = null, int? acctive = null)
        {
            User user = null;
            string res = "";
            if (id.HasValue) { user = _DbConText.Users.Find(id); }
            if (!string.IsNullOrEmpty(name)) { user = _DbConText.Users.FirstOrDefault(x => x.UserName.Contains(name)); }
            if (user != null)
            {
                res += user.UserID.ToString() + " " + user.UserName;
                user.Active = acctive;
                _DbConText.Users.Update(user);
                _DbConText.SaveChanges();
            }
            if (string.IsNullOrEmpty(res)) { return " không tìm thấy User"; }
            return $"đã update {res}";
        }
        private string UpdateAddressIP(int? id = null, string? name = null, int? acctive = null)
        {
            Address address = null;
            string res = "";
            if (id.HasValue) { address = _DbConText.Addresses.Find(id); }
            if (!string.IsNullOrEmpty(name)) { address = _DbConText.Addresses.FirstOrDefault(x => x.AddressIP.Contains(name)); }
            if (address != null)
            {
                res += address.AddressID.ToString() + " " + address.AddressIP;
                address.Active = acctive;
                _DbConText.Addresses.Update(address);
                _DbConText.SaveChanges();
            }
            if (string.IsNullOrEmpty(res)) { return " không tìm thấy IP nào"; }
            return $"đã update {res}";
        }
        /// <summary>
        /// update báo cáo hệ thống
        /// </summary>
        /// <param name="NumberOfCoinSystem">tổng số tiền ở hệ thống</param>
        /// <param name="NumberOfRemainingCoin">tổng số tiền còn lại ở user</param>
        private void UpdateManagerAdd_coin(int NumberOfCoinSystem, int NumberOfRemainingCoin)
        {
            var Manager = _DbConText.Managers.FirstOrDefault();
            Manager.NumberOfCoinSystem += NumberOfCoinSystem;
            Manager.NumberOfRemainingCoin += NumberOfRemainingCoin;
            _DbConText.Managers.Update(Manager);
            _DbConText.SaveChanges();
        }
        /// <summary>
        /// update báo cáo trừ tiền ở hệ thống
        /// </summary>
        /// <param name="NumberOfCoinSystem">+ số tiền user đã sử dụng ở hệ thống </param>
        /// <param name="NumberOfRemainingCoin">- tổng số tiền còn lại của user trong hệ thống </param>
        private void UpdateManagerMinus_coin(int NumberOfCoinSystem, int NumberOfRemainingCoin)
        {
            var Manager = _DbConText.Managers.FirstOrDefault();
            Manager.NumberOfUsedCoin += NumberOfCoinSystem;
            Manager.NumberOfRemainingCoin -= NumberOfRemainingCoin;
            _DbConText.Managers.Update(Manager);
            _DbConText.SaveChanges();
        }
        private string Add_or_Minus_Coin(string name, int coin, int Add_or_Minus)
        {
            var user = _DbConText.Users.FirstOrDefault(x => x.UserName.Contains(name));
            string res = "";
            if (user != null)
            {
                switch (Add_or_Minus)
                {
                    case 0:
                        res += name + " có " + user.Coin.ToString() + " đã trừ ";
                        user.Coin -= coin;
                        _DbConText.Users.Update(user);
                        _DbConText.SaveChanges();
                        res += $" {coin} là {name} có {user.Coin} coin";
                        break;
                    case 1:
                        res += name + " có " + user.Coin.ToString() + " đã cộng ";
                        user.Coin += coin;
                        _DbConText.Users.Update(user);
                        _DbConText.SaveChanges();
                        res += $" {coin} là {name} có {user.Coin} coin";
                        break;
                    default:
                        break;
                }
            }
            if (string.IsNullOrEmpty(res)) { return "khong tim thay tai khoan"; }
            return res;
        }
        #endregion
        public async Task<List<Translate>> Get(SystemManager systemManager)
        {
            var res = _DbConText.Translates.Include(x => x.User).ThenInclude(x => x.Address).ToList();
            if (systemManager.AddressID.HasValue) { res = res.Where(x => x.User.AddressID == systemManager.AddressID).ToList(); }
            if (!string.IsNullOrEmpty(systemManager.Address)) { res = res.Where(x => x.User.Address.AddressIP.Contains(systemManager.Address)).ToList(); }
            if (systemManager.UserID.HasValue) { res = res.Where(x => x.UserID == systemManager.UserID).ToList(); }
            if (!string.IsNullOrEmpty(systemManager.UserName)) { res = res.Where(x => x.User.UserName.Contains(systemManager.UserName)).ToList(); }
            if (!string.IsNullOrEmpty(systemManager.TranslateCode)) { res = res.Where(x => x.TranslateCode.Contains(systemManager.TranslateCode)).ToList(); }
            if (systemManager.TimeFrom.HasValue) { res = res.Where(x => x.TimeTranslates >= systemManager.TimeFrom).ToList(); }
            if (systemManager.TimeTo.HasValue) { res = res.Where(x => x.TimeTranslates <= systemManager.TimeTo).ToList(); }
            return res;
        }

        public async Task<string> UnOrBlock(UnlockOrBock? UnlockOrBock = null, int? acctive = null)
        {
            string res = UpdateUser(UnlockOrBock.UserID, UnlockOrBock.UserName, acctive);
            res += UpdateAddressIP(UnlockOrBock.AddressID, UnlockOrBock.Address, acctive);
            return res;
        }

        public async Task<string> Add_Coin(string name, int coin)
        {
            UpdateManagerAdd_coin(coin, coin);
            return Add_or_Minus_Coin(name, coin, 1);
        }

        public async Task<string> Minus_Coin(string name, int coin)
        {
            UpdateManagerMinus_coin(coin, coin);
            return Add_or_Minus_Coin(name, coin, 0);
        }
        public async Task<Manager> Get_Infor_Manager()
            => await _DbConText.Managers.FirstOrDefaultAsync();

        public IQueryable<Translate> Get_Order(int? userID)
        {
            var query = _DbConText.Translates.AsQueryable();
            if (userID.HasValue) { query = query.Where(x => x.UserID == userID); }
            return query;
        }
    }
}
