using System;
using System.Linq;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Collections.Generic;
using DesafioGamificacaoCPFL.Infra.Database.Settings;

namespace DesafioGamificacaoCPFL.Infra.Database.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly IMongoCollection<Cliente> _clientes;

        public ClienteRepository(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.);

            _clientes = database.GetCollection<Cliente>(settings.ClientesCollectionName);
        }

        public async Task<string> Create(Cliente cliente)
        {
            await _clientes.InsertOneAsync(cliente);
            return cliente.Id;
        }

        public async Task<Cliente> Get(string id)
        {
            var cliente = await _clientes.FindAsync<Cliente>(cliente => cliente.Id == id);

            if (cliente == null)
                throw new OperationCanceledException($"Cliente não encontrado com o id {id}");

            return cliente.First();
        }

        public async Task<List<Cliente>> GetAll()
        {
            var clientes = await _clientes.FindAsync<Cliente>(cliente => true);
            return clientes?.ToList();
        }   
    }
}
