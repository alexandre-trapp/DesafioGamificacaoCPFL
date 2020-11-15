using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using DesafioGamificacaoCPFL.Infra.Database.Repositories;
using DesafioGamificacaoCPFL.Models;

namespace DesafioGamificacaoCPFL.Controllers
{
    [ApiController]
    [Route("")]
    public class PontuacaoClienteController : ControllerBase
    {
        private readonly ILogger<PontuacaoClienteController> _logger;
        private readonly IPontuacaoClienteRepository _pontuacaoRepository;

        public PontuacaoClienteController(ILogger<PontuacaoClienteController> logger,
            IPontuacaoClienteRepository pontuacaoRepository)
        {
            _logger = logger;
            _pontuacaoRepository = pontuacaoRepository;
        }

        [HttpPost("pontuacaoCliente/cadastrar/")]
        public async Task<ActionResult> Create(PontuacaoCliente pontuacaoCliente)
        {
            await _pontuacaoRepository.Create(pontuacaoCliente);
            return NoContent();
        }

        [HttpGet("pontuacaoCliente/{clienteId}")]
        public async Task<ActionResult<PontuacaoCliente>> Get(string clienteId)
        {
            return Ok(await _pontuacaoRepository.Get(clienteId));
        }

        [HttpPost("pontuacaoCliente/atualizar")]
        public async Task<ActionResult> Update(PontuacaoCliente pontuacaoCliente)
        {
            await _pontuacaoRepository.Update(pontuacaoCliente);
            return NoContent();
        }

        [HttpDelete("pontuacaoCliente/deletar")]
        public async Task<ActionResult> Delete(string clienteId)
        {
            await _pontuacaoRepository.Delete(clienteId);
            return NoContent();
        }
    }
}
