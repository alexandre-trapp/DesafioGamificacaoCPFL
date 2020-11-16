using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using DesafioGamificacaoCPFL.Models;
using DesafioGamificacaoCPFL.Services;
using DesafioGamificacaoCPFL.Infra.Database.Repositories;

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

        /// <summary>
        /// No cadastro inicial de pontos, informar os pontos iniciais no campo
        /// quantidadeNovosPontos, não informar nada no campo quantidadePontosAtual, o mesmo será atualizado
        /// com a informação do campo quantidadeNovosPontos.
        /// </summary>
        /// <param name="pontuacaoCliente"></param>
        /// <returns></returns>
        [HttpPost("pontuacaoCliente/cadastrar/")]
        public async Task<ActionResult> Create(PontuacaoCliente pontuacaoCliente)
        {
            pontuacaoCliente.QuantidadePontosAtual = pontuacaoCliente.QuantidadeNovosPontos;
            pontuacaoCliente.QuantidadeXP = pontuacaoCliente.QuantidadeNovosPontos;
            pontuacaoCliente.QuantidadePontosXpNecessariosParaAtingirProximoNivel = PontuacaoClienteService.PONTUACAO_INICIAL_PARA_PASSAR_PRIMEIRO_NIVEL;

            await _pontuacaoRepository.Create(pontuacaoCliente);
            return NoContent();
        }

        [HttpGet("pontuacaoCliente/{clienteId}")]
        public async Task<ActionResult<PontuacaoCliente>> Get(string clienteId)
        {
            return Ok(await _pontuacaoRepository.Get(clienteId));
        }

        [HttpPost("pontuacaoCliente/adicionarPontosAoCliente")]
        public async Task<ActionResult<PontuacaoClienteResponse>> AdicionarPontosAoCliente(AdicionarPontuacaoClienteRequest pontuacaoCliente)
        {
            var pontuacaoClienteService = new PontuacaoClienteService(_pontuacaoRepository);
            return Ok(await pontuacaoClienteService.AdicionarPontosAoClienteConformeRegraDeGamificacao(pontuacaoCliente));
        }

        [HttpPost("pontuacaoCliente/resgatarPontosDoCliente")]
        public async Task<ActionResult> ResgatarPontosDoCliente(ResgatePontosRequest resgatePontosCliente)
        {
            var pontuacaoClienteService = new PontuacaoClienteService(_pontuacaoRepository);
            return Ok(await pontuacaoClienteService.ResgatarPontosDoCliente(resgatePontosCliente));
        }

        [HttpDelete("pontuacaoCliente/deletar")]
        public async Task<ActionResult> Delete(string clienteId)
        {
            await _pontuacaoRepository.Delete(clienteId);
            return NoContent();
        }
    }
}
