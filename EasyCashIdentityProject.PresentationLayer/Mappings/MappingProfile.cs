using AutoMapper;
using EasyCashIdentityProject.DTO.Dtos.CustomerAccountProcessDtos;
using EasyCashIdentityProject.EntityLayer.Concrete;

namespace EasyCashIdentityProject.PresentationLayer.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<SendMoneyDto, CustomerAccountProcess>();
            CreateMap<CustomerAccountProcess, SendMoneyDto>();
        }
    }
}
