using System.ComponentModel.DataAnnotations.Schema;
using webapi.Models;

namespace webapi.DTO.Tasks
{
    public class ListTasksDTO
    {
        public string? IDTASK { get; set; }
        public string? IDUSER { get; set; }
        public string? IDRULE { get; set; }
        public string? IDFLAG { get; set; }
        public string? PROJECT { get; set; }
        public string? EVENT_TASK { get; set; }
        public DateTime? CREATEDATE { get; set; }
        public DateTime? UPDATEDATE { get; set; }
        [ForeignKey("IDUSER")]
        public virtual USERS? USERS { get; set; }
        [ForeignKey("ISORULE")]
        public virtual ISORULE? ISORULE { get; set; }
        [ForeignKey("IDFLAGS")]
        public virtual FLAGS? FLAGS { get; set; }
    }
}
