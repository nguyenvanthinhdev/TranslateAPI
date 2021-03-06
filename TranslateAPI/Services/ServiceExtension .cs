using TranslateAPI.ConText;

namespace TranslateAPI.Services
{
    public class ServiceExtension
    {
        private readonly AppDbContext _DbConText;
        public ServiceExtension() { _DbConText = new AppDbContext(); }
        public void UpdateAddress(int IDIP)
        {
            var adress = _DbConText.Addresses.FirstOrDefault(x => x.AddressID == IDIP);
            adress.NumberOfUsers += 1;
            _DbConText.Addresses.Update(adress);
            _DbConText.SaveChanges();

        }
    }
}
