using GestaoInvestimentos.Application.DTOs.Usuario.Request;
using GestaoInvestimentos.Application.Interfaces;
using GestaoInvestimentos.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoInvestimentos.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("autenticar")]
        public async Task<IActionResult> AutenticarUsuario([FromBody] AutenticarUsuarioRequest autenticarUsuario)
        {
            try
            {
                var usuario = await _usuarioService.AutenticarUsuario(autenticarUsuario.Email, autenticarUsuario.Senha);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [AllowAnonymous]
        [Route("cadastrar")]
        public async Task<IActionResult> CadastrarUsuario([FromBody] UsuarioRequest usuario)
        {
            try
            {
                await _usuarioService.CadastrarUsuario(usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Authorize]
        [Route("listar")]
        public async Task<IActionResult> ListarUsuarios()
        {
            try
            {
                var usuarios = await _usuarioService.ListarUsuario();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
