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

        [HttpGet("{id}")]
        public ActionResult<Cliente> Get(int id)
        {
            return new Cliente
            {
                Cpf = "1234",
                Nome = "ronaldo"
            };
        }
    }
}
