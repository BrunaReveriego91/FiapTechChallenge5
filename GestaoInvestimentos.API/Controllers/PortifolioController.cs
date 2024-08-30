using GestaoInvestimentos.Application.Interfaces;
using GestaoInvestimentos.Domain.Entitites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoInvestimentos.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PortifolioController : ControllerBase
    {
        private readonly IPortifolioService _portifolioService;

        public PortifolioController(IPortifolioService portifolioService)
        {
            _portifolioService = portifolioService;
        }


        [HttpPost]
        [Authorize]
        [Route("cadastrar")]
        public async Task<IActionResult> CadastrarPortifolio([FromBody] Portifolio portifolio)
        {
            try
            {
                await _portifolioService.CadastrarPortifolio(portifolio);
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
        public async Task<IActionResult> ListarPortifolios()
        {
            try
            {
                var portifolios = await _portifolioService.ListarPortifolios();
                return Ok(portifolios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Authorize]
        [Route("{id:Guid}")]
        public async Task<IActionResult> BuscarPortifolio(Guid id)
        {
            try
            {
                var portifolios = await _portifolioService.BuscarPortifolio(id);
                return Ok(portifolios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Authorize]
        [Route("{id:Guid}")]
        public async Task<IActionResult> RemoverPortifolio(Guid id)
        {
            try
            {
                await _portifolioService.RemoverPortifolio(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
