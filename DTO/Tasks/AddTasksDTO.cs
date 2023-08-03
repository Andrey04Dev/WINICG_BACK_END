namespace webapi.DTO.Tasks
{
    public class AddTasksDTO
    {
        public string? IDUSER { get; set; }
        public string? IDRULE { get; set; }
        public string? IDFLAG { get; set; }
        public string? PROJECT { get; set; }
        public string? EVENT_TASK { get; set; }
        public DateTime? CREATEDATE { get; set; }
        public DateTime? UPDATEDATE { get; set; }
    }
}
