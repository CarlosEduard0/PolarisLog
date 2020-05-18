using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PolarisLog.Infra.CrossCutting.Identity.Model;
using PolarisLog.WebApi.Payloads.Usuario;
using PolarisLog.WebApi.Services;

namespace PolarisLog.WebApi.Controllers
{
    [ApiController]
    [Route("usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;

        public UsuarioController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            TokenService tokenService,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar(CadastrarUsuarioPayload cadastrarUsuarioPayload)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(entry => entry.Errors).Select(error => error.ErrorMessage));
            }
            
            var applicationUser = _mapper.Map<ApplicationUser>(cadastrarUsuarioPayload);

            var result = await _userManager.CreateAsync(applicationUser, cadastrarUsuarioPayload.Senha);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(error => error.Description));
            }
            
            return Ok();
        }

        [HttpPost("logar")]
        public async Task<IActionResult> Logar(LogarPayload logarPayload)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(entry => entry.Errors).Select(error => error.ErrorMessage));
            }

            var result = await _signInManager.PasswordSignInAsync(logarPayload.Email, logarPayload.Senha, false, true);
            if (!result.Succeeded)
            {
                return BadRequest(new [] {"Email ou senha inválidos"});
            }

            var user = await _userManager.FindByEmailAsync(logarPayload.Email);
            var token = _tokenService.GenerateToken(user.Id);
            return Ok(new {token});
        }
    }
}