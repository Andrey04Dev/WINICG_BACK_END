using AutoMapper;
using webapi.DTO.Risks;
using webapi.Models;

namespace webapi.Mapper
{
    public class RiskMapper: Profile
    {
        public RiskMapper()
        {
            CreateMap<AddRisksDTO, RISKS>().ReverseMap();
        }
    }
}
