using AutoMapper;
using webapi.DTO.Users;
using webapi.Models;

namespace webapi.Mapper
{
    public class UsersMapper:Profile
    {
        public UsersMapper()
        {
            CreateMap<USERS, ListUserDTO>().ReverseMap();
            CreateMap<ListUserDTO, USERS>().ReverseMap();
            CreateMap<AddUserDTO, USERS>().ReverseMap();
            CreateMap<UpdateUserDTO, USERS>().ReverseMap();
        }
    }
}
