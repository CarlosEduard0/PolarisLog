using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PolarisLog.Application.Interfaces;
using PolarisLog.Application.ViewModels;
using PolarisLog.Domain.Entities;
using PolarisLog.Domain.Notifications;
using PolarisLog.WebApi.Payloads;

namespace PolarisLog.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogController : ControllerBase
    {
        private readonly ILogAppService _logAppService;
        private readonly DomainNotificationHandler _notificationHandler;

        public LogController(ILogAppService logAppService, INotificationHandler<DomainNotification> domainNotificationHandler)
        {
            _logAppService = logAppService;
            _notificationHandler = (DomainNotificationHandler) domainNotificationHandler;
        }

        public async Task<IActionResult> ObterTodos()
        {
            var logs = await _logAppService.ObterTodos();
            var logsPayload = logs.Select(log => new LogPayload
            {
                Level = log.Level.ToString(),
                Descricao = log.Descricao,
                Origem = log.Origem,
                CadastradoEm = log.CadastradoEm.ToLocalTime()
            }); 
            return Ok(logsPayload);
        }
        
        [HttpPost]
        public async Task<IActionResult> Adicionar(LogPayload logPlPayload)
        {
            var logViewModel = new LogViewModel
            {
                Descricao = logPlPayload.Descricao,
                Origem = logPlPayload.Origem
            };

            Enum.TryParse(typeof(Level), logPlPayload.Level, out var level);
            logViewModel.Level = (Level?) level;
            
            var id = await _logAppService.Adicionar(logViewModel);
            if (_notificationHandler.TemNotificacao())
            {
                return BadRequest(_notificationHandler.ObterNotificacoes());
            }
            
            return Ok(new {id});
        }

        [HttpPost("arquivar/{id}")]
        public async Task<IActionResult> Arquivar(string id)
        {
            Guid.TryParse(id, out var guid);
            await _logAppService.Arquivar(guid);
            if (_notificationHandler.TemNotificacao())
            {
                return BadRequest(_notificationHandler.ObterNotificacoes());
            }

            return Ok();
        }
    }
}