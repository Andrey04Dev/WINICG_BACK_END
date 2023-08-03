using AutoMapper;
using webapi.DTO.Audits;
using webapi.Models;

namespace webapi.Mapper
{
    public class AuditsMapper:Profile
    {
        public AuditsMapper()
        {
            CreateMap<AddAuditsDTO, AUDITS>().ReverseMap();
            CreateMap<ListAuditsDTO, AUDITS>().ReverseMap();
            CreateMap<AUDITS, AddAuditsDTO>().ReverseMap();
        }
    }
}
