using AutoMapper;
using LogManager.Api.ViewModels;
using LogManager.Business.Models;

namespace LogManager.Api.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<RequestLog, RequestLogViewModel>().ReverseMap();
        }
    }
}
