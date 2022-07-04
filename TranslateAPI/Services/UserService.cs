using TranslateAPI.ConText;
using TranslateAPI.Entities;
using TranslateAPI.InterFaces;

namespace TranslateAPI.Services
{
    public class UserService : IUser
    {
        private readonly AppDbContext _DbConText;
        public UserService(AppDbContext DbConText) { _DbConText = DbConText; }

        private Address UpdateAddress(Address address, int NumberOfUsers) 
        {
            address.NumberOfUsers += NumberOfUsers;
            return address;
        }
        private Address CreateIP(string IP) 
        {
            Address address = new Address()
            {
                AddressIP = IP,
                CreateTimeIP = DateTime.Now,
                NumberOfUses = 0,
                NumberOfUsers = 1,
                Active = 1
            };
            return address;
        }
        private User CreateUser(string Name,string password,int IDIP) 
        {
            User user = new User()
            {
                UserName = Name,
                password = password,
                AddressID = IDIP,
                Active = 1,
                Coin = 20000,
                CreateTimeUser = DateTime.Now,
                NumberOfuses = 0,
                PypeUser = 1
            };
            return user;
        }

        private Manager UpdateManager(Manager manager,int NumberIpSystem) 
        {
            manager.NumberIpSystem += NumberIpSystem;
            manager.NumberOfUsersSystem += 1;
            manager.NumberOfCoinSystem += 20000;
            manager.NumberOfRemainingCoin += 20000;
            return manager;
        }



        public async Task<string> CreateUser(string Name,string Password)
        {
            var manager = _DbConText.Managers.FirstOrDefault();
            string IP = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.GetValue(0).ToString();
            string result = "đã tạo tài khoản thành công ! ";
            var addressIp = _DbConText.Addresses.FirstOrDefault(x => x.AddressIP == IP);
            if (_DbConText.Users.Any(x => x.UserName == Name)) { return "tài khoản đã tồn tại"; }
            if (addressIp != null) 
            {
                if (addressIp.Active == 0) { return $"ip : {IP} đã bị block : v"; }
                else
                {
                    _DbConText.AddAsync(CreateUser(Name, Password, addressIp.AddressID));
                    _DbConText.Addresses.Update(UpdateAddress(addressIp, 1));
                    _DbConText.Managers.Update(UpdateManager(manager, 0));
                    await _DbConText.SaveChangesAsync();
                }
            }
            else
            {
                addressIp = CreateIP(IP);
                _DbConText.Addresses.Add(addressIp);
                _DbConText.SaveChanges();
                _DbConText.Add(CreateUser(Name, Password, addressIp.AddressID));
                _DbConText.Managers.Update(UpdateManager(manager, 1));
                await _DbConText.SaveChangesAsync();
            }
            return result;
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
