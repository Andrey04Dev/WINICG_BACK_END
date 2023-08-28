using AutoMapper;
using webapi.DTO.Isorule;
using webapi.Models;

namespace webapi.Mapper
{
    public class IsoRuleMapper:Profile
    {
        public IsoRuleMapper()
        {
            CreateMap<AddIsoruleDTO, ISORULE>().ReverseMap();
            CreateMap<UpdateIsoRuleDTO, ISORULE>().ReverseMap();
            CreateMap<ListIsoruleDTO,ISORULE>().ReverseMap();
        }
    }
}
