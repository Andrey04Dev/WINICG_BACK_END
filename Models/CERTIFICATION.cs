using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    public class CERTIFICATION
    {
        public string? IDCERTIFICATION { get; set; }
        public  string? CERTIFICATION_NAME { get; set; }
        public DateTime? CERTIFICACION_DATE { get; set; }
        public string? PERSONCHANGE { get; set; } = null;
        public DateTime? CREATEDATE { get; set; }
        public DateTime? UPDATEDATE { get; set; }
    }
}
