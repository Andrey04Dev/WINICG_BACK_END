namespace webapi.DTO.Isorule
{
    public class AddIsoruleDTO
    {
        public string? IDRULE { get; set; }
        public string? IDCERTIFICATION { get; set; }
        public string? IDAUDIT { get; set; }
        public string? NAMERULE { get; set; }
        public string? CODERULE { get; set; }
        public string? RULE_DESCRIPTION { get; set; }
        public DateTime? CREATEDATE { get; set; }
        public DateTime? UPDATEDATE { get; set; }
    }
}
