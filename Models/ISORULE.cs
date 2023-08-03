using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models
{
    public class ISORULE
    {
        [Key]
        public string? IDISORULE { get; set; }
        public string? IDCERTIFICATION { get; set; }
        public string? IDAUDIT { get; set; }
        public string? NAMERULE { get; set; }
        public string? CODERULE { get; set; }
        public string? RULE_DESCRIPTION { get; set; }
        public DateTime? CREATEDATE { get; set; }
        public DateTime? UPDATEDATE { get; set; }
        [ForeignKey("IDAUDIT")]
        public virtual AUDITS? AUDITS { get; set; }
        [ForeignKey("IDCERTIFICATION")]
        public virtual CERTIFICATION? CERTIFICATION { get; set; }
    }
}
