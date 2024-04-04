using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models
{
    public class PROCESS
    {
        [Key]
        public string? IDPROCESS { get; set; }
        public string? IDRULE { get; set; }
        public string? PROCESSNAME { get; set; }
        public string? CHARGE_PERSON { get; set; }
        public string? ROLE_INVOLVES { get; set; }
        public string? PERSONCHANGE { get; set; }
        public DateTime? CREATEDATE { get; set; }
        public DateTime? UPDATEDATE { get; set; }
        [ForeignKey("IDISORULE")]
        public virtual ISORULE? ISORULE { get; set; }  
    }
}
