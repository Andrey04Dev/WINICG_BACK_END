using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models
{
    public class COMPANY_POSITION
    {
        [Key]
        public string? IDCOMPANY_POSITION { get; set; }
        public string? IDUSER { get; set; }
        public string? IDPROCESS { get; set; }
        public string? MANDATED { get; set; }
        public string? DESCRIPTION { get; set; }
        public string? RESPONSABILITIES { get; set; }
        public string? PROFILE_POSITION { get; set; }
        public string? PERSONCHANGE { get; set; }
        public DateTime? CREATEDATE { get; set; }
        public DateTime? UPDATEDATE { get; set; }
        [ForeignKey("IDUSER")]
        public virtual USERS? USERS { get; set; }
        [ForeignKey("IDPROCESS")]
        public virtual PROCESS? PROCESS { get; set; }
    }
}
