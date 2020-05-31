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
                .ForMember(usuario => usuario.UserName, mf => mf.MapFrom(pay => pay.Email));

            CreateMap<CadastrarLogPayload, LogViewModel>()
                .ForMember(viewModel => viewModel.UsuarioId, opts => opts.Ignore())
                .ForMember(viewModel => viewModel.AmbienteId, opts => opts.Ignore())
                .ForMember(viewModel => viewModel.NivelId, opts => opts.Ignore());
            
            CreateMap<QueryPayload, QueryViewModel>();
            CreateMap<LogQueryPayload, LogQueryViewModel>();
        }
    }
}