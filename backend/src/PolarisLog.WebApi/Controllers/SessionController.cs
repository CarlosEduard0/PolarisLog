using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PolarisLog.Application.Interfaces;
using PolarisLog.Domain.Notifications;
using PolarisLog.WebApi.Payloads;
using PolarisLog.WebApi.Services;

namespace PolarisLog.WebApi.Controllers
{
    [ApiController]
    [Route("sessions")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionAppService _sessionAppService;
        private readonly DomainNotificationHandler _notificationHandler;
        private readonly TokenService _tokenService;

        public SessionController(
            TokenService tokenService,
            ISessionAppService sessionAppService,
            INotificationHandler<DomainNotification> notificationHandler)
        {
            _tokenService = tokenService;
            _sessionAppService = sessionAppService;
            _notificationHandler = (DomainNotificationHandler) notificationHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginPayload loginPayload)
        {
            var userId = await _sessionAppService.Logar(loginPayload.Email, loginPayload.Senha);

            if (_notificationHandler.TemNotificacao())
            {
                return BadRequest(_notificationHandler.ObterNotificacoes());
            }

            var token = _tokenService.GenerateToken(userId);
            return Ok(new {token});
        }
    }
}