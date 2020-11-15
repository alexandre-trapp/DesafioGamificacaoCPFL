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
    public class PagamentoParcialController : ControllerBase
    {
        private readonly ILogger<PagamentoParcialController> _logger;
        private readonly IPagamentoParcialRepository _pagamentoParcialRepository;
        private readonly IClienteRepository _clienteRepository;

        public PagamentoParcialController(ILogger<PagamentoParcialController> logger,
            IPagamentoParcialRepository pagamentoParcialRepository,
            IClienteRepository clienteRepository)
        {
            _logger = logger;
            _pagamentoParcialRepository = pagamentoParcialRepository;
            _clienteRepository = clienteRepository;
        }

        [HttpPost("pagamentoParcial/cadastrarNovoPagamento/")]
        public async Task<ActionResult<PagamentoParcialResponse>> CadastrarNovoPagamento(PagamentoParcial pagamentoParcial)
        {
            return Ok(await PagamentoParcialService.CadastrarNovoPagamentoParcial(
                _pagamentoParcialRepository, 
                _clienteRepository, 
                pagamentoParcial));
        }

        [HttpGet("pagamentoParcial/consultarPagamentoParcialDoCliente/{clienteId}")]
        public async Task<ActionResult<PagamentoParcial>> ConsultarPagamentoParcialDoCliente(string clienteId)
        {
            return Ok(await _pagamentoParcialRepository.ConsultarPagamentoParcialDoCliente(clienteId));
        }
    }
}
