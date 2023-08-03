using System.ComponentModel.DataAnnotations.Schema;
using webapi.Models;

namespace webapi.DTO.Flags
{
    public class ListFlagsDTO
    {
        public string? IDFLAGS { get; set; }
        public string? IDRULE { get; set; }
        public string? FLAGNAME { get; set; }
        public DateTime? CREATEDATE { get; set; }
        public DateTime? UPDATEDATE { get; set; }
        [ForeignKey("IDRULE")]
        public virtual ISORULE? ISORULE { get; set; }
    }
}
