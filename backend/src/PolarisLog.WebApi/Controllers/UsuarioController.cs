using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NETCore.MailKit.Core;
using PolarisLog.Infra.CrossCutting.Identity.Model;
using PolarisLog.WebApi.Payloads.Usuario;
using PolarisLog.WebApi.Services;
using PolarisLog.WebApi.ViewModels;

namespace PolarisLog.WebApi.Controllers
{
    [ApiController]
    [Route("Usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public UsuarioController(
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            TokenService tokenService,
            IMapper mapper,
            IEmailService emailService)
        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar(CadastrarUsuarioPayload cadastrarUsuarioPayload)
        {
            var applicationUser = _mapper.Map<ApplicationUser>(cadastrarUsuarioPayload);

            var result = await _userManager.CreateAsync(applicationUser, cadastrarUsuarioPayload.Senha);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(error => error.Description));
            }

            return Ok(new[] {"Usuário cadastrado com sucesso"});
        }

        [HttpPost("Logar")]
        public async Task<IActionResult> Logar(LogarPayload logarPayload)
        {
            var result = await _signInManager.PasswordSignInAsync(logarPayload.Email, logarPayload.Senha, false, false);
            if (!result.Succeeded)
            {
                return BadRequest(new[] {"Email ou senha inválidos"});
            }

            var user = await _userManager.FindByEmailAsync(logarPayload.Email);
            var token = _tokenService.GenerateToken(user.Id);
            return Ok(new LogarViewModel {AccessToken = token, Usuario = _mapper.Map<UsuarioViewModel>(user)});
        }

        [HttpPost("RecuperarSenha")]
        public async Task<IActionResult> RecuperarSenha(RecuperarSenhaPayload recuperarSenhaPayload)
        {
            var applicationUser = await _userManager.FindByEmailAsync(recuperarSenhaPayload.Email);
            if (applicationUser == null)
            {
                return BadRequest(new[] {"Nenhum usuário cadastrado com este email"});
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(applicationUser);
            var applicationUrl = _configuration.GetValue<string>("ApplicationUrl");
            await _emailService.SendAsync(applicationUser.Email, "PolarisLog - Recuperar senha",
                $"Para trocar sua senha <a href=\"{applicationUrl}/RecuperarSenha?email={applicationUser.Email}&token={token}\">clique aqui</a>",
                true);
            return Ok(new[] {"Foi enviado um email com as instruções para recuperar a senha"});
        }

        [HttpPost("ResetarSenha")]
        public async Task<IActionResult> ResetarSenha(ResetarSenhaPayload resetarSenhaPayload)
        {
            var applicationUser = await _userManager.FindByEmailAsync(resetarSenhaPayload.Email);
            if (applicationUser == null)
            {
                return BadRequest(new[] {"Nenhum usuário cadastrado este email"});
            }

            var result = await _userManager.ResetPasswordAsync(applicationUser, resetarSenhaPayload.Token, resetarSenhaPayload.Senha);
            if (result.Errors.Any())
            {
                return BadRequest(result.Errors.Select(error => error.Description));
            }

            return Ok(new[] {"Senha alterada com sucesso"});
        }
    }
}