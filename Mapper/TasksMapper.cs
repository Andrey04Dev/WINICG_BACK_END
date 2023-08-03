using AutoMapper;
using webapi.DTO.Tasks;
using webapi.Models;

namespace webapi.Mapper
{
    public class TasksMapper: Profile
    {
        public TasksMapper()
        {
            CreateMap<TASKS, ListTasksDTO>().ReverseMap();
        }
    }
}
