using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models
{
    public class FILES
    {
        [Key]
        public string? IDFILE { get; set; }
        [ForeignKey("IDAUDITS")]
        public string? IDMODULE { get; set; }
        public string? EXTENSION { get; set; }
        public string? URLFILE { get; set; }
        public byte[]? BINARY_FILE { get; set; }
        public DateTime? CREATEDATE { get; set; }
        public DateTime? UPDATEDATE { get; set; }
    }
}
