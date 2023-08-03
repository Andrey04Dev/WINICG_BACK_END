using AutoMapper;
using webapi.DTO.Roles;
using webapi.Models;

namespace webapi.Mapper
{
    public class RolesMapper: Profile
    {
        public RolesMapper() {
            CreateMap<ROLES,ListRolesDTO>().ReverseMap();
            CreateMap<AddRolesDTO, ROLES>();
        }
    }
}
