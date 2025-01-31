﻿using System;
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
            var logQuery = _mapper.Map<LogQueryViewModel>(logQueryPayload);

            Guid.TryParse(logQueryPayload.AmbienteId, out var ambienteId);
            logQuery.AmbienteId = ambienteId;
            
            var logs = await _logAppService.ObterTodos(logQuery);
            
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

        [HttpPut("Arquivar/{id:guid}")]
        public async Task<IActionResult> Arquivar(Guid id)
        {
            await _logAppService.Arquivar(id);
            if (_notificationHandler.TemNotificacao())
            {
                return BadRequest(_notificationHandler.ObterNotificacoes());
            }

            return Ok();
        }
        
        [HttpPut]
        public async Task<IActionResult> ArquivarPorIds(ArquivarLogPayload arquivarLogPayload)
        {
            await _logAppService.Arquivar(arquivarLogPayload.Ids);
            if (_notificationHandler.TemNotificacao())
            {
                return BadRequest(_notificationHandler.ObterNotificacoes());
            }

            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            await _logAppService.Deletar(id);
            if (_notificationHandler.TemNotificacao())
            {
                return BadRequest(_notificationHandler.ObterNotificacoes());
            }

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletarPorIds([FromQuery] Guid[] ids)
        {
            await _logAppService.Deletar(ids);
            if (_notificationHandler.TemNotificacao())
            {
                return BadRequest(_notificationHandler.ObterNotificacoes());
            }

            return Ok();
        }
    }
}