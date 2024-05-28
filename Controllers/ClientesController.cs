using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using DesafioGamificacaoCPFL.Infra.Database.Repositories;

namespace DesafioGamificacaoCPFL.Controllers
{
    [ApiController]
    [Route("")]
    public class ClientesController : ControllerBase
    {
        private readonly ILogger<ClientesController> _logger;
        private readonly IClienteRepository _clienteRepository;

        public ClientesController(ILogger<ClientesController> logger,
            IClienteRepository clienteRepository)
        {
            _logger = logger;
            _clienteRepository = clienteRepository;
        }

        [HttpPost("clientes/cadastrar")]
        public async Task<ActionResult<string>> Create(Cliente cliente)
        {
            return Ok(await _clienteRepository.Create(cliente));
        }

        [HttpGet("clientes/{id}")]
        public async Task<ActionResult<Cliente>> Get(string id)
        {
            return Ok(await _clienteRepository.Get(id));
        }

        [HttpGet("clientes/teste")]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetAll()
        {
            return Ok(new List<Cliente>
            {
                new Cliente
                {
                    Id = "1",
                    Nome = "teste1"
                },
                new Cliente
                {
                    Id = "2",
                    Nome = "teste2"
                },
                new Cliente
                {
                    Id = "3",
                    Nome = "teste3"
                },
            });
        }

        [HttpGet("clientes/listarTodos")]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetAll()
        {
            return Ok(await _clienteRepository.GetAll());
        }

        [HttpDelete("clientes/{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _clienteRepository.Delete(id);
            return NoContent();
        }
    }
}
