using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    public class CERTIFICATION
    {
        public string? IDCERTIFICATION { get; set; }
        public  string? CERTIFICATION_NAME { get; set; }
        public DateTime? CERTIFICATION_DATE { get; set; }
        public DateTime? CREATEDATE { get; set; }
        public DateTime? UPDATEDATE { get; set; }
    }
}
