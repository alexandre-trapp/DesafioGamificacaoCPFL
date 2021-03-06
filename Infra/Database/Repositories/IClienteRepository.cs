﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DesafioGamificacaoCPFL.Infra.Database.Repositories
{
    public interface IClienteRepository
    {
        Task<Cliente> Get(string id);
        Task<IEnumerable<Cliente>> GetAll();
        Task<string> Create(Cliente cliente);
        Task Delete(string id);
    }
}
