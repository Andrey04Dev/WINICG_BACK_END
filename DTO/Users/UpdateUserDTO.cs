namespace webapi.DTO.Users
{
    public class UpdateUserDTO
    {
        public string? IDUSER { get; set; }
        public string? IDROLE { get; set; }
        public string? CEDULA { get; set; }
        public string? FULLNAME { get; set; }
        public string? EMAIL { get; set; }
        public string? PASSWORD { get; set; }
        public bool? ACTIVE { get; set; }
    }
}
