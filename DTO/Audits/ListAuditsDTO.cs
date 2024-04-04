namespace webapi.DTO.Audits
{
    public class ListAuditsDTO
    {
        public string? IDAUDIT { get; set; }
        public string? AUDIT_NAME { get; set; }
        public DateTime? AUDIT_DATE { get; set; }
        public TimeSpan? AUDIT_TIME { get; set; }
        public string? AUDIT_SUBJECT { get; set; }
        public int NUMBER_DAYS { get; set; }
        public string? KIND_AUDIT { get; set; }
        public string? SCOPE_AUDIT { get; set; }
        public string? AUDIT_PROCESS { get; set; }
        public string? AUDIT_RULE { get; set; }
        public DateTime? CREATEDATE { get; set; }
        public DateTime? UPDATEDATE { get; set; }
    }
}
