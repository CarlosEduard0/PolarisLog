using AutoMapper;
using PolarisLog.Application.ViewModels;
using PolarisLog.Infra.CrossCutting.Identity.Model;
using PolarisLog.WebApi.Payloads;
using PolarisLog.WebApi.Payloads.Log;
using PolarisLog.WebApi.Payloads.Usuario;

namespace PolarisLog.WebApi.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CadastrarUsuarioPayload, ApplicationUser>()
                .ForMember(app => app.UserName, mf => mf.MapFrom(pay => pay.Email))
                .ForMember(app => app.Email, mf => mf.MapFrom(pay => pay.Email));
            
            CreateMap<LogQueryPayload, LogQueryViewModel>();
        }
    }
}