using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models
{
    public class TASKS
    {
        [Key]
        public string? IDTASK { get; set; }
        public string? IDUSER { get; set; }
        public string? IDRULE { get; set; }
        public string? IDFLAGS { get; set; }
        public string? PROJECT { get; set; }
        public string? EVENT_TASK { get; set; }
        public DateTime? CREATEDATE { get; set; }
        public DateTime? UPDATEDATE { get; set; }
        public virtual USERS? USERS { get; set; }
        public virtual ISORULE? ISORULE { get; set; }
        public virtual FLAGS? FLAGS { get; set; } 
    }
}
