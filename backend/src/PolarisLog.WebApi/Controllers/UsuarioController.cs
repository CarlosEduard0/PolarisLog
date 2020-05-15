using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PolarisLog.Application.Interfaces;
using PolarisLog.Application.ViewModels;

namespace PolarisLog.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioAppService _usuarioAppService;

        public UsuarioController(IUsuarioAppService usuarioAppService)
        {
            _usuarioAppService = usuarioAppService;
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar(UsuarioViewModel usuarioViewModel)
        {
            var notifications = await _usuarioAppService.Adicionar(usuarioViewModel);
            if (notifications.Any())
            {
                return BadRequest(notifications);
            }
            
            return Ok(usuarioViewModel);
        }
    }
}