using System.ComponentModel.DataAnnotations.Schema;
using webapi.Models;

namespace webapi.DTO.Complete_Process_Task
{
    public class ListCompleteTask
    {
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
