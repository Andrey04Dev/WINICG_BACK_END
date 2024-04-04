using System.ComponentModel.DataAnnotations.Schema;
using webapi.Models;

namespace webapi.DTO.Users
{
    public class ListUserDTO
    {
        public string? IDUSER { get; set; }
        public string? IDROLE { get; set; }
        public string? IDPOSITION { get; set; }
        public string? CEDULA { get; set; }
        public string? FULLNAME { get; set; }
        public string? EMAIL { get; set; }
        public byte[]? PASSWORDHASH { get; set; }
        public byte[]? PASSWORDSALT { get; set; }
        public bool? ACTIVE { get; set; }
        public string? PERSONCHANGE { get; set; }
        public DateTime? CREATEDATE { get; set; }
        public DateTime? UPDATEDATE { get; set; }
        public virtual ROLES? ROLE { get; set; }
        public virtual POSITION? POSITION { get; set; }
    }
}
