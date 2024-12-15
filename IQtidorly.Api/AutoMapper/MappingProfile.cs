using AutoMapper;
using IQtidorly.Api.Models.Users;
using IQtidorly.Api.ViewModels.Users;

namespace IQtidorly.Api.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Users
            CreateMap<User, CreateUserViewModel>().ReverseMap();
        }
    }
}
