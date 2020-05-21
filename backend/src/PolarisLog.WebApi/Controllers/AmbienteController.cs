using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PolarisLog.Application.Interfaces;
using PolarisLog.Application.ViewModels;
using PolarisLog.WebApi.Payloads;

namespace PolarisLog.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Ambientes")]
    public class AmbienteController : ControllerBase
    {
        private readonly IAmbienteAppService _ambienteAppService;
        private readonly IMapper _mapper;

        public AmbienteController(IAmbienteAppService ambienteAppService, IMapper mapper)
        {
            _ambienteAppService = ambienteAppService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos([FromQuery] QueryPayload queryPayload)
        {
            var ambientes = await _ambienteAppService.ObterTodos(_mapper.Map<QueryViewModel>(queryPayload));
            
            var metadata = new
            {
                ambientes.TotalCount,
                ambientes.PageSize,
                ambientes.CurrentPage,
                ambientes.TotalPages,
                ambientes.HasNext,
                ambientes.HasPrevious
            };
            
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            
            return Ok(ambientes);
        }
    }
}