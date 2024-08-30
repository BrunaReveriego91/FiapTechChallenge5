using GestaoInvestimentos.Application.Interfaces;
using GestaoInvestimentos.Domain.Entitites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoInvestimentos.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransacaoController : ControllerBase
    {
        private readonly ITransacaoService _transacaoService;

        public TransacaoController(ITransacaoService transacaoService)
        {
            _transacaoService = transacaoService;
        }


        [HttpPost]
        [Authorize]
        [Route("cadastrar")]
        public async Task<IActionResult> CadastrarTransacao([FromBody] Transacao transacao)
        {
            try
            {
                await _transacaoService.CadastrarTransacao(transacao);
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
        public async Task<IActionResult> ListarTransacaos()
        {
            try
            {
                var transacaos = await _transacaoService.ListarTransacoes();
                return Ok(transacaos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Authorize]
        [Route("{id:Guid}")]
        public async Task<IActionResult> BuscarTransacao(Guid id)
        {
            try
            {
                var transacaos = await _transacaoService.BuscarTransacao(id);
                return Ok(transacaos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Authorize]
        [Route("{id:Guid}")]
        public async Task<IActionResult> RemoverTransacao(Guid id)
        {
            try
            {
                await _transacaoService.RemoverTransacao(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
