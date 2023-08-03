using System.ComponentModel.DataAnnotations.Schema;
using webapi.Models;

namespace webapi.DTO.Isorule
{
    public class ListIsoruleDTO
    {
        public string? IDRULE { get; set; }
        public string? IDCERTIFICATION { get; set; }
        public string? IDAUDIT { get; set; }
        public string? NAMERULE { get; set; }
        public string? CODERULE { get; set; }
        public string? RULE_DESCRIPTION { get; set; }
        public DateTime? CREATEDATE { get; set; }
        public DateTime? UPDATEDATE { get; set; }
        public virtual AUDITS? AUDITS { get; set; }
        public virtual CERTIFICATION? CERTIFICATION { get; set; }
    }
}
