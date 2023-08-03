using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    public class ROLES
    {
        [Key]
        public string? IDROLE { get; set; }
        public string? ROLE { get; set; }
        public DateTime? CREATEDATE { get; set; }
        public DateTime? UPDATEDATE { get; set; }
    }
}
