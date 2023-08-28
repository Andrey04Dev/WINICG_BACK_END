using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models
{
    public class NO_ACCORDANCE
    {
        [Key]
        public string? IDACCORDANCE { get; set; }
        public string? IDPROCESS { get; set; }
        public string? IDAUDIT { get; set; }
        public string? IDTASK { get; set; }
        public string? NAME_NO_ACCORDANCE { get; set; }
        public string? KIND { get; set; }
        public string? RANKING { get; set; }
        public string? DESCRIPTION { get; set; }
        public bool? STATE { get; set; }
        public string? AUDIT_DETECT { get; set; }
        public DateTime? CREATEDATE { get; set; }
        public DateTime? UPDATEDATE { get; set; }
        [ForeignKey("IDPROCESS")]
        public virtual PROCESS? PROCESS { get; set; }
        public virtual AUDITS? AUDITS { get; set; }
        public virtual TASKS? TASKS { get; set; }
    }
}
