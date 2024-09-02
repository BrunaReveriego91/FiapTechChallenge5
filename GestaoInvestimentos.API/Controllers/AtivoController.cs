using GestaoInvestimentos.Application.Interfaces;
using GestaoInvestimentos.Domain.Entitites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoInvestimentos.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AtivoController : ControllerBase
    {
        private readonly IAtivoService _ativoService;

        public AtivoController(IAtivoService ativoService)
        {
            _ativoService = ativoService;
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("cadastrar")]
        public async Task<IActionResult> CadastrarAtivo([FromBody] Ativo ativo)
        {
            try
            {
                await _ativoService.CadastrarAtivo(ativo);
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
        public async Task<IActionResult> ListarAtivos()
        {
            try
            {
                var ativos = await _ativoService.ListarAtivos();
                return Ok(ativos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Authorize]
        [Route("{id:Guid}")]
        public async Task<IActionResult> BuscarAtivo(Guid id)
        {
            try
            {
                var ativos = await _ativoService.BuscarAtivo(id);
                return Ok(ativos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("{id:Guid}")]
        public async Task<IActionResult> RemoverAtivo(Guid id)
        {
            try
            {
                await _ativoService.RemoverAtivo(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
