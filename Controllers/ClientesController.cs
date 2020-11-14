using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace DesafioGamificacaoCPFL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly ILogger<ClientesController> _logger;

        public ClientesController(ILogger<ClientesController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public ActionResult<Cliente> Create(Cliente cliente)
        {
            return new Cliente
            {
                Cpf = "162.108.960-65",
                Nome = "Delcio"
            };
        }

        [HttpGet("{id}")]
        public ActionResult<Cliente> Get(int id)
        {
            return new Cliente
            {
                Cpf = "162.108.960-65",
                Nome = "Delcio"
            };
        }

        [HttpGet()]
        public ActionResult<IEnumerable<Cliente>> GetAll()
        {
            // todo
            return new List<Cliente>();
        }
    }
}
