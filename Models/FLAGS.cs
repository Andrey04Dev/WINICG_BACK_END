using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models
{
    public class FLAGS
    {
        [Key]
        public string? IDFLAG { get; set; }
        public string? IDRULE { get; set; }
        public string? FLAGNAME { get; set; }
        public string? PERSONCHANGE { get; set; }
        public DateTime? CREATEDATE { get; set; }
        public DateTime? UPDATEDATE { get; set; }
        [ForeignKey("IDISORULE")]
        public virtual ISORULE? ISORULE { get; set; }
    }
}
