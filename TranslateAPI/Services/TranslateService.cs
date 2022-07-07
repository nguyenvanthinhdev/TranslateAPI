using System.Text;
using System.Text.RegularExpressions;
using TranslateAPI.ConText;
using TranslateAPI.Entities;
using TranslateAPI.InterFaces;
using HttpRequest = xNet.HttpRequest;
using SHA1Managed = System.Security.Cryptography.SHA1Managed;
namespace TranslateAPI.Services
{
    public class TranslateService :ServiceExtension, ITranslate
    {
        private readonly AppDbContext _DbConText;
        public TranslateService(AppDbContext DbConText) : base(DbConText) { }
        #region private

        private string Createhashcode(string text)
        {
            SHA1Managed sha1 = new SHA1Managed();
            byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(text));
            StringBuilder hashs1 = new StringBuilder();
            foreach (byte x in hash)
            {
                hashs1.Append(x.ToString("x2"));
            }
            return hashs1.ToString();
        }
        private async Task<string> Translateapigoole(string? InLanguage = null, string? outLanguage = null, string? Translate = null)
        {
            if (string.IsNullOrEmpty(InLanguage) || string.IsNullOrEmpty(outLanguage) || string.IsNullOrEmpty(Translate))
            { throw new Exception("thieu"); }

            HttpRequest http = new HttpRequest();
            string html = http.Get($"https://translate.googleapis.com/translate_a/single?client=gtx&sl={InLanguage}&tl={outLanguage}&dt=t&q={Translate}").ToString();
            Regex regex = new Regex(@$"\[\[\[\"".*?(.*?)\""{Translate}\""");
            string[] res = regex.Match(html).ToString().Split("\"");
            return res[1];
        }
        private async Task<Translate> CreateTranslate(int userID,TranslateGG translate) 
        {
            return new Translate()
            {
                UserID = userID,
                inpLanguage = translate.Inlanguage,
                outLanguage = translate.OutLanguage,
                Input = translate.Input,
                Result = await Translateapigoole(translate.Inlanguage, translate.OutLanguage, translate.Input),
                TimeTranslates = DateTime.Now,
                TranslateCode = Createhashcode($"{userID}+{DateTime.Today.Hour.ToString()}"),
            };
        }
        private void UpdateUser(User user,int coin) 
        {
            user.Coin -= coin;
            user.NumberOfuses += 1;
            _DbConText.Users.Update(user);
            _DbConText.SaveChanges();
        }
        private void UpdateManager(int NumberOfUsedCoin) 
        {
            var Manager = _DbConText.Managers.FirstOrDefault();
            Manager.NumberOfUsesSystem += 1;
            Manager.NumberOfUsedCoin += NumberOfUsedCoin;
            Manager.NumberOfRemainingCoin -= NumberOfUsedCoin;
            _DbConText.Managers.Update(Manager);
            _DbConText.SaveChanges();
        }
        #endregion

        public async Task<Translate> TranslateAPI(Account account,TranslateGG translate) 
        {
            if (!(_DbConText.Users.Where(x => x.UserName == account.UserName).Any(x => x.password == account.PassWork))){ throw new Exception("sai tài khoản"); }
            var acc = _DbConText.Users.FirstOrDefault(x => x.UserName == account.UserName);
            if(_DbConText.Addresses.Where(x=>x.AddressID == acc.AddressID).Any(x => x.Active == 0)){ throw new Exception("IP bị block"); }
            if(acc.Active == 0) { throw new Exception("account bị block"); }
            // type acc   1 = thường | 2 = CTV | 3 = Admin 
            switch (acc.PypeUser)
            {
                case 1:
                    UpdateUser(acc, 1000);
                    UpdateManager(1000);
                    break;
                case 2:
                    UpdateUser(acc, 700);
                    UpdateManager(700);
                    break;
                case 3:
                    UpdateUser(acc, 0);
                    UpdateManager(0);
                    break;
                default:
                    break;
            }           
            UpdateAddress(acc.AddressID);
            Translate trans = await CreateTranslate(acc.UserID, translate);
            _DbConText.Add(trans);
            _DbConText.SaveChanges();

            return trans;
        }
    }
}
