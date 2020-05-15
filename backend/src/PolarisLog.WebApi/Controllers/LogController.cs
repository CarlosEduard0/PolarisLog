using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PolarisLog.Application.Interfaces;
using PolarisLog.Application.ViewModels;
using PolarisLog.Domain.Entities;
using PolarisLog.WebApi.Payloads;

namespace PolarisLog.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogController : ControllerBase
    {
        private readonly ILogAppService _logAppService;

        public LogController(ILogAppService logAppService)
        {
            _logAppService = logAppService;
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar(AdicionarLogPayload logPlPayload)
        {
            var logViewModel = new LogViewModel
            {
                Descricao = logPlPayload.Descricao,
                Origem = logPlPayload.Origem
            };

            Enum.TryParse(typeof(Level), logPlPayload.Level, out var level);
            logViewModel.Level = (Level?) level;
            
            var notifications = await _logAppService.Adicionar(logViewModel);
            if (notifications.Any())
            {
                return BadRequest(notifications);
            }
            
            return Ok();
        }
    }
}