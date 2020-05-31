using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PolarisLog.Application.Interfaces;
using PolarisLog.Application.ViewModels;
using PolarisLog.Domain.Notifications;
using PolarisLog.WebApi.Payloads.Log;

namespace PolarisLog.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Logs")]
    public class LogController : ControllerBase
    {
        private readonly ILogAppService _logAppService;
        private readonly DomainNotificationHandler _notificationHandler;
        private readonly IMapper _mapper;

        public LogController(ILogAppService logAppService, INotificationHandler<DomainNotification> domainNotificationHandler, IMapper mapper)
        {
            _logAppService = logAppService;
            _mapper = mapper;
            _notificationHandler = (DomainNotificationHandler) domainNotificationHandler;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos([FromQuery] LogQueryPayload logQueryPayload)
        {
            var logs = await _logAppService.ObterTodos(_mapper.Map<LogQueryViewModel>(logQueryPayload));
            
            var metadata = new
            {
                logs.TotalCount,
                logs.PageSize,
                logs.CurrentPage,
                logs.TotalPages,
                logs.HasNext,
                logs.HasPrevious
            };
            
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            
            return Ok(logs);
        }
        
        [HttpPost]
        public async Task<IActionResult> Adicionar(CadastrarLogPayload cadastrarLogPayload)
        {
            var logViewModel = _mapper.Map<LogViewModel>(cadastrarLogPayload);
            var usuarioId = (User.Identity as ClaimsIdentity)?.FindFirst(ClaimTypes.NameIdentifier).Value;
            logViewModel.UsuarioId = Guid.Parse(usuarioId);
            
            Guid.TryParse(cadastrarLogPayload.AmbienteId, out var ambienteId);
            logViewModel.AmbienteId = ambienteId;

            Guid.TryParse(cadastrarLogPayload.NivelId, out var nivelId);
            logViewModel.NivelId = nivelId;
            
            var id = await _logAppService.Adicionar(logViewModel);
            if (_notificationHandler.TemNotificacao())
            {
                return BadRequest(_notificationHandler.ObterNotificacoes());
            }
            
            return Ok(new {id});
        }

        [HttpPost("Arquivar/{id}")]
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(string id)
        {
            Guid.TryParse(id, out var guid);
            await _logAppService.Deletar(guid);
            if (_notificationHandler.TemNotificacao())
            {
                return BadRequest(_notificationHandler.ObterNotificacoes());
            }

            return Ok();
        }
    }
}