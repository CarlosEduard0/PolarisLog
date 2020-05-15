using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
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
        public async Task<IActionResult> Adicionar(Usuario usuario)
        {
            var notifications = await _usuarioAppService.Adicionar(usuario);

            if (notifications.Any())
            {
                return BadRequest(notifications);
            }
            
            return Ok(usuario);
        }
    }
}