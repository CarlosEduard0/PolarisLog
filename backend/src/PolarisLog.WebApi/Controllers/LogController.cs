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
using PolarisLog.Domain.Entities;
using PolarisLog.Domain.Notifications;
using PolarisLog.WebApi.Payloads;
using PolarisLog.WebApi.Payloads.Log;

namespace PolarisLog.WebApi.Controllers
{
    [ApiController]
    [Route("logs")]
    [Authorize]
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
        public async Task<IActionResult> ObterTodos([FromQuery] QueryPayload queryPayload)
        {
            var logs = await _logAppService.ObterTodos(_mapper.Map<QueryViewModel>(queryPayload));
            
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
        public async Task<IActionResult> Adicionar(CadastrarLogPayload cadastrarLogPlPayload)
        {
            var logViewModel = new LogViewModel
            {
                Descricao = cadastrarLogPlPayload.Descricao,
                Origem = cadastrarLogPlPayload.Origem
            };

            Enum.TryParse(typeof(Level), cadastrarLogPlPayload.Level, out var level);
            logViewModel.Level = (Level?) level;

            var usuarioId = (User.Identity as ClaimsIdentity)?.FindFirst(ClaimTypes.NameIdentifier).Value;
            logViewModel.UsuarioId = Guid.Parse(usuarioId);
            
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