using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models
{
    public class USERS
    {
        [Key]
        public string? IDUSER { get; set; }
        public string? IDROLE { get; set; }
        public string? IDPOSITION { get; set; }
        public string? CEDULA { get; set; }
        public string? FULLNAME { get; set; } 
        public string? EMAIL { get; set; }
        public byte[]? PASSWORDHASH { get; set; }
        public byte[]? PASSWORDSALT { get; set; }
        public bool? ACTIVE { get; set; }
        public string? CHANGEPERSON { get; set; }
        public DateTime? CREATEDATE { get; set; }
        public DateTime? UPDATEDATE { get; set; }
        [ForeignKey("IDROLE")]
        public virtual ROLES? ROLE { get; set; }
        public virtual POSITION? POSITION { get; set; }
    }
}
