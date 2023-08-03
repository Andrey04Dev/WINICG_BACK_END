namespace webapi.DTO.Risks
{
    public class ListRisksDTO
    {
        public string? IDRISKS { get; set; }
        public string? IDRULE { get; set; }
        public string? NAMERISKS { get; set; }
        public string? CONSEQUENSE { get; set; }
        public string? SOURCE_RISK { get; set; }
        public DateTime? CREATEDATE { get; set; }
        public DateTime? UPDATEDATE { get; set; }
    }
}
