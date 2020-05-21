using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PolarisLog.Application.Interfaces;
using PolarisLog.Application.ViewModels;
using PolarisLog.WebApi.Payloads.Log;

namespace PolarisLog.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Niveis")]
    public class NivelController : ControllerBase
    {
        private readonly INivelAppService _nivelAppService;
        private readonly IMapper _mapper;

        public NivelController(INivelAppService nivelAppService, IMapper mapper)
        {
            _nivelAppService = nivelAppService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos([FromQuery] LogQueryPayload logQueryPayload)
        {
            var niveis = await _nivelAppService.ObterTodos(_mapper.Map<LogQueryViewModel>(logQueryPayload));
            
            var metadata = new
            {
                niveis.TotalCount,
                niveis.PageSize,
                niveis.CurrentPage,
                niveis.TotalPages,
                niveis.HasNext,
                niveis.HasPrevious
            };
            
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            
            return Ok(niveis);
        }
    }
}