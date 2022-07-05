using System.Text;
using System.Text.RegularExpressions;
using TranslateAPI.ConText;
using TranslateAPI.Entities;
using TranslateAPI.InterFaces;
using HttpRequest = xNet.HttpRequest;
using SHA1Managed = System.Security.Cryptography.SHA1Managed;
namespace TranslateAPI.Services
{
    public class TranslateService : ITranslate
    {
        private readonly AppDbContext _DbConText;
        public TranslateService(AppDbContext DbConText)
        {
            _DbConText = DbConText;
        }
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
        private void UpdateUser(User user) 
        {
            user.Coin -= 1000;
            user.NumberOfuses += 1;
            _DbConText.Users.Update(user);
            _DbConText.SaveChanges();
        }
        private void UpdateAddressIP(int IDIP) 
        {
            var adress = _DbConText.Addresses.FirstOrDefault(x => x.AddressID == IDIP);
            adress.NumberOfUses += 1;
            _DbConText.Addresses.Update(adress);
            _DbConText.SaveChanges();
        }
        private void UpdateManager() 
        {
            var Manager = _DbConText.Managers.FirstOrDefault();
            Manager.NumberOfUsesSystem += 1;
            Manager.NumberOfUsedCoin += 1000;
            Manager.NumberOfRemainingCoin -= 1000;
            _DbConText.Managers.Update(Manager);
            _DbConText.SaveChanges();
        }
        #endregion

        public async Task<Translate> Translate(Account account,TranslateGG translate) 
        {
            if (!(_DbConText.Users.Where(x => x.UserName == account.UserName).Any(x => x.password == account.PassWork))){ throw new Exception("sai tài khoản"); }
            var acc = _DbConText.Users.FirstOrDefault(x => x.UserName == account.UserName);
            UpdateUser(acc);
            UpdateAddressIP(acc.AddressID);
            UpdateManager();
            await _DbConText.SaveChangesAsync();
            return await CreateTranslate(acc.UserID, translate);
        }
    }
}
