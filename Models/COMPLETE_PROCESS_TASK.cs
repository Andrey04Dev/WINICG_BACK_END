using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models
{
    public class COMPLETE_PROCESS_TASK
    {
        [Key]
        public string? IDCOMPLETE { get; set; }
        public string? IDPROCESS { get; set; }
        public string? IDFLAGS { get; set; }
        public string? COMPLETED { get; set; }
        public DateTime? CREATEDATE { get; set; }
        public DateTime? UPDATEDATE { get; set; }
        [ForeignKey("IDPROCESS")]
        public virtual PROCESS? PROCESS { get; set; }
        [ForeignKey("IDFLAGS")]
        public virtual FLAGS? FLAGS { get; set; }
    }
}
