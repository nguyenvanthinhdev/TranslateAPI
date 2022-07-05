using TranslateAPI.ConText;
using TranslateAPI.Entities;
using TranslateAPI.InterFaces;

namespace TranslateAPI.Services
{
    public class UserService : IUser
    {
        #region private 
        private readonly AppDbContext _DbConText;
        public UserService(AppDbContext DbConText) { _DbConText = DbConText; }
        private void UpdateAddress(Address address) 
        {
            address.NumberOfUsers += 1;
            _DbConText.Addresses.Update(address);
            _DbConText.SaveChanges();
            
        }
        private Address CreateIP(Manager manager, string IP) 
        {
            manager.NumberIpSystem += 1;
            _DbConText.Managers.Update(manager);
            var a = new Address()
            {
                AddressIP = IP,
                CreateTimeIP = DateTime.Now,
                NumberOfUses = 0,
                NumberOfUsers = 1,
                Active = 1
            };
            _DbConText.Addresses.Update(a);
            _DbConText.SaveChanges();

            return a;

        }
        private User CreateUser(Account account,int IDIP) 
        {
            return new User()
            {
                UserName = account.UserName,
                password = account.PassWork,
                AddressID = IDIP,
                Active = 1,
                Coin = 20000,
                CreateTimeUser = DateTime.Now,
                NumberOfuses = 0,
                PypeUser = 1
            };
        }
        private void UpdateManager(Manager manager,int NumberIpSystem) 
        {
            manager.NumberIpSystem += NumberIpSystem;
            manager.NumberOfUsersSystem += 1;
            manager.NumberOfCoinSystem += 20000;
            manager.NumberOfRemainingCoin += 20000;
        }
        private string CheckCreateUser(Account account,string Password) 
        {
            return (String.Compare(account.UserName, account.PassWork, true) == 0) ? "tài khoản và mật khẩu không được giống nhau" : String.Compare(account.UserName, account.PassWork, true) == 0 ? "đã tạo tài khoản thành công!" : "mật khẩu phải giống nhau";
        }

        #endregion

        public async Task<string> CreateUser(Account account,string Password)
        {
            if (_DbConText.Users.Any(x => x.UserName == account.UserName)) { return "tài khoản đã tồn tại"; }
            string result = CheckCreateUser(account, Password);
            var manager = _DbConText.Managers.FirstOrDefault();
            string IP = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.GetValue(0).ToString();

            if (result.Contains("đã tạo tài khoản thành công!")) 
            {
                var addressIp = _DbConText.Addresses.FirstOrDefault(x => x.AddressIP == IP);
                if (addressIp == null)
                {
                    addressIp = CreateIP(manager, IP);
                }
                if (addressIp.Active == 0) { return $"ip : {IP} đã bị block : v"; }
                else
                {
                    _DbConText.Add(CreateUser(account, addressIp.AddressID));
                    UpdateAddress(addressIp);
                    UpdateManager(manager, 0);
                    _DbConText.SaveChanges();
                }
            }
            return result;
        }
        public async Task<string> Login(Account account) 
        {
            if(_DbConText.Users.Where(x => x.UserName == account.UserName).Any(x => x.password == account.PassWork)) { return "login thành công"; }
            return "tai khoan hoac mat khau khong chinh sac";
        }
        public async Task<string> ChangerPassword(Account account, string PassWork)
        {
            string res = "Doi Mat Khau Thanh Cong";
            var acc = _DbConText.Users.FirstOrDefault(x => x.UserName == account.UserName);
            if (acc == null) { return $"tai khoan {account.UserName} khong ton tai"; }
            if(!(String.Compare(acc.password, account.PassWork, true) == 0)) 
            {
                return "mat khau sai";
            }
            if (String.Compare(acc.password, PassWork, true) == 0) { res = "mật khẩu cũ và mới phải khác nhau"; }
            else
            {
                acc.password = PassWork;
                _DbConText.Update(acc);
                _DbConText.SaveChanges();
            }
            return res;
        }
        public async Task<List<User>> GetUser(int? UserID = null, string? Name = null)
        {
            var Users = _DbConText.Users.ToList();
            if (UserID.HasValue) { Users = _DbConText.Users.Where(x => x.UserID == UserID).ToList(); }
            if (!string.IsNullOrEmpty(Name)) { Users = _DbConText.Users.Where(x => x.UserName == Name).ToList(); }
            return Users;
        }

    }
}
