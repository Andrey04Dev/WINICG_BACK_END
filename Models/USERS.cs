using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models
{
    public class USERS
    {
        [Key]
        public string? IDUSER { get; set; }
        public string? IDROLE { get; set; }
        public string? ID { get; set; }
        public string? EMAIL { get; set; }
        public DateTime? CREATEDATE { get; set; }
        public DateTime? UPDATEDATE { get; set; }
        [ForeignKey("IDROLE")]
        public virtual ROLES? ROLE { get; set; }
    }
}
