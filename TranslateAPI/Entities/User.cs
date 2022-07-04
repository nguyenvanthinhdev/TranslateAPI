using System.ComponentModel.DataAnnotations;

namespace TranslateAPI.Entities
{
    public class User
    {
        public int UserID { get; set; }
        public int AddressID { get; set; }
        [StringLength(30)]
        public string UserName { get; set; }//tên ng dùng
        [StringLength(30)]
        public string password { get; set; }
        public DateTime? CreateTimeUser { get; set; }
        public int? NumberOfuses { get; set; }//so lan da dung,
        public double Coin { get; set; }

        public int PypeUser { get; set; } //Type // user thường;ctv,admin[1,2,3]

        public int? Active { get; set; }//cho phép dùng hay ko

        public virtual Address? Address { get; set; }
        public virtual IEnumerable<Translate>? Translates { get; set; }
    }
}
