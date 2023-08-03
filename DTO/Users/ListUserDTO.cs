using System.ComponentModel.DataAnnotations.Schema;
using webapi.Models;

namespace webapi.DTO.Users
{
    public class ListUserDTO
    {
        public string? IDUSER { get; set; }
        public string? IDROLE { get; set; }
        public string? ID { get; set; }
        public string? EMAIL { get; set; }
        public DateTime? CREATEDATE { get; set; }
        public DateTime? UPDATEDATE { get; set; }
        public virtual ROLES? ROLE { get; set; }
    }
}
