using System.ComponentModel.DataAnnotations.Schema;
using webapi.Models;

namespace webapi.DTO.Users
{
    public class AddUserDTO
    {
        public string? IDROLE { get; set; }
        public string? IDPOSITION { get; set; }
        public string? CEDULA { get; set; }
        public string? FULLNAME { get; set; }
        public string? EMAIL { get; set; }
        public string? PASSWORD { get; set; }
    }
}
