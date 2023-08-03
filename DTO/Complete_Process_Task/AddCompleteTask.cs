using System.ComponentModel.DataAnnotations.Schema;
using webapi.Models;

namespace webapi.DTO.Complete_Process_Task
{
    public class AddCompleteTask
    {
        public string? IDCOMPLETE { get; set; }
        public string? IDPROCESS { get; set; }
        public string? IDFLAGS { get; set; }
        public string? COMPLETED { get; set; }
        public DateTime? CREATEDATE { get; set; }
        public DateTime? UPDATEDATE { get; set; }
    }
}
