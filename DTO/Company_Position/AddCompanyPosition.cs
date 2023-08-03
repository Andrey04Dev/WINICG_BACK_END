using System.ComponentModel.DataAnnotations.Schema;
using webapi.Models;

namespace webapi.DTO.Company_Position
{
    public class AddCompanyPosition
    {
        public string? IDCOMPANY_POSITION { get; set; }
        public string? IDUSER { get; set; }
        public string? IDPROCESS { get; set; }
        public string? MANDATED { get; set; }
        public string? DESCRIPTION { get; set; }
        public string? RESPONSABILITIES { get; set; }
        public string? PROFILE_POSITION { get; set; }
        public DateTime? CREATEDATE { get; set; }
        public DateTime? UPDATEDATE { get; set; }
    }
}
