using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PolarisLog.Application.Interfaces;
using PolarisLog.Application.ViewModels;
using PolarisLog.Domain.Notifications;
using PolarisLog.WebApi.Payloads;

namespace PolarisLog.WebApi.Controllers
{
    [ApiController]
    [Route("usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioAppService _usuarioAppService;
        private readonly DomainNotificationHandler _notificationHandler;

        public UsuarioController(IUsuarioAppService usuarioAppService, INotificationHandler<DomainNotification> notificationHandler)
        {
            _usuarioAppService = usuarioAppService;
            _notificationHandler = (DomainNotificationHandler) notificationHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar(CadastrarUsuarioPayload cadastrarUsuarioPayload)
        {
            var id = await _usuarioAppService.Adicionar(new UsuarioViewModel
            {
                Nome = cadastrarUsuarioPayload.Nome,
                Email = cadastrarUsuarioPayload.Email,
                Senha = cadastrarUsuarioPayload.Senha,
                SenhaConfirmacao = cadastrarUsuarioPayload.SenhaConfirmacao
            });
            
            if (_notificationHandler.TemNotificacao())
            {
                return BadRequest(_notificationHandler.ObterNotificacoes());
            }
            
            return Ok(new {id});
        }
    }
}