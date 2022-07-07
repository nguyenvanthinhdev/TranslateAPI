using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TranslateAPI.Entities
{
    public class User
    {
        public int UserID { get; set; }
        [Required]
        public int AddressID { get; set; }
        [Required]
        [StringLength(30)]
        [MinLength(6)]
        public string UserName { get; set; }//tên ng dùng
        [Required]
        [StringLength(30)]
        [MinLength(6)]
        public string password { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CreateTimeUser { get; set; }
        public int? NumberOfuses { get; set; }//so lan da dung,
        public double Coin { get; set; }

        public int PypeUser { get; set; } //Type // user thường;ctv,admin[1,2,3]

        public int? Active { get; set; }//cho phép dùng hay ko

        public virtual Address? Address { get; set; }
        public virtual IEnumerable<Translate>? Translates { get; set; }
    }
}
