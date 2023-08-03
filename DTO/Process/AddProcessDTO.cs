namespace webapi.DTO.Process
{
    public class AddProcessDTO
    {
        public string? IDRULE { get; set; }
        public string? CODEPROCESS { get; set; }
        public string? CHARGE_PERSON { get; set; }
        public string? ROLE_INVOLVES { get; set; }
        public DateTime? CREATEDATE { get; set; }
        public DateTime? UPDATEDATE { get; set; }
    }
}
