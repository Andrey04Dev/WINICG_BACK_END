using System.ComponentModel.DataAnnotations.Schema;
using webapi.Models;

namespace webapi.DTO.No_Accordance
{
    public class ListNoAccordanceDTO
    {
        public string? IDACCORDANCE { get; set; }
        public string? IDPROCESS { get; set; }
        public string? NAME_NO_ACCORDANCE { get; set; }
        public string? KIND { get; set; }
        public string? RANKING { get; set; }
        public string? AUDIT_DETECT { get; set; }
        public DateTime? CREATEDATE { get; set; }
        public DateTime? UPDATEDATE { get; set; }
        public virtual PROCESS? PROCESS { get; set; }
    }
}
