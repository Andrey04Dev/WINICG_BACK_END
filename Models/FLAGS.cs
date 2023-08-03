using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models
{
    public class FLAGS
    {
        [Key]
        public string? IDFLAGS { get; set; }
        public string? IDISORULE { get; set; }
        public string? FLAGNAME { get; set; }
        public DateTime? CREATEDATE { get; set; }
        public DateTime? UPDATEDATE { get; set; }
        [ForeignKey("IDISORULE")]
        public virtual ISORULE? ISORULE { get; set; }
    }
}
