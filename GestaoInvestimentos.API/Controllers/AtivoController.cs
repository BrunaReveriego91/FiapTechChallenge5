using GestaoInvestimentos.Application.Interfaces;
using GestaoInvestimentos.Domain.Entitites;
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
    }
}
