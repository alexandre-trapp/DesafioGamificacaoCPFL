using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace DesafioGamificacaoCPFL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly ILogger<ClienteController> _logger;

        public ClienteController(ILogger<ClienteController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id:length(24)}")]
        public Cliente Get(string id)
        {
            return new Cliente
            {
                Cpf = "1234",
                Nome = "ronaldo"
            };
        }
    }
}
