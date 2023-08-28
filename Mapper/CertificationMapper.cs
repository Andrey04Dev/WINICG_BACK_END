using AutoMapper;
using webapi.DTO.Certification;
using webapi.Models;

namespace webapi.Mapper
{
    public class CertificationMapper:Profile
    {
        public CertificationMapper()
        {
            CreateMap<AddCertificationDTO, CERTIFICATION>().ReverseMap();
        }
    }
}
