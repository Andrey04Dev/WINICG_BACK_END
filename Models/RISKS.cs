using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models
{
    public class RISKS
    {
        [Key] 
        public string?  IDRISKS { get; set; }
        public string? IDRULE { get; set; }
        public string? NAMERISKS { get; set; }
        public string? CONSEQUENSE { get; set; }
        public string? SOURCE_RISK { get; set; }
        public DateTime? CREATEDATE { get; set; }
        public DateTime? UPDATEDATE { get; set; }
        [ForeignKey("IDISORULE")]
        public virtual ISORULE? ISORULE { get; set; }
    }
}
