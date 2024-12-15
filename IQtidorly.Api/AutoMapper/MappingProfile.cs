using AutoMapper;
using IQtidorly.Api.Models.AgeGroups;
using IQtidorly.Api.Models.Users;
using IQtidorly.Api.ViewModels.AgeGroups;
using IQtidorly.Api.ViewModels.Users;

namespace IQtidorly.Api.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Users
            CreateMap<User, CreateUserViewModel>().ReverseMap();

            // AgeGroups
            CreateMap<AgeGroup, AgeGroupForCreateModel>().ReverseMap();
            CreateMap<AgeGroup, AgeGroupForUpdateModel>().ReverseMap();
            CreateMap<AgeGroup, AgeGroupForGetModel>().ReverseMap();
        }
    }
}
