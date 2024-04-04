using AutoMapper;
using webapi.DTO.History;
using webapi.Models;

namespace webapi.Mapper
{
    public class HistorialMapper: Profile
    {
        public HistorialMapper()
        {
            CreateMap<AddHistoryDTO, HISTORIAL>().ReverseMap();
        }
    }
}
