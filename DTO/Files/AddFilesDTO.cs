namespace webapi.DTO.Files
{
    public class AddFilesDTO
    {
        public string? id { get; set; }
        public List<IFormFile>? files { get; set; }
    }
}
