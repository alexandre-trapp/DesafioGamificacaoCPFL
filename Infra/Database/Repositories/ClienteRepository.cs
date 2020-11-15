using System;
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
            var database = client.GetDatabase(settings.DatabaseName);

            _clientes = database.GetCollection<Cliente>(settings.ClientesCollectionName);
        }

        public async Task<string> Create(Cliente cliente)
        {
            await _clientes.InsertOneAsync(cliente);
            return cliente.Id;
        }

        public async Task Delete(string id) =>
            await _clientes.DeleteOneAsync(cliente => cliente.Id == id);

        public async Task<Cliente> Get(string id)
        {
            var buscaCliente = await _clientes.FindAsync<Cliente>(cliente => cliente.Id == id);
            var cliente = buscaCliente.FirstOrDefault();

            if (cliente == null)
                throw new OperationCanceledException($"Cliente com o id {id} não encontrado no sistema.");

            return cliente;
        }

        public async Task<IEnumerable<Cliente>> GetAll()
        {
            var clientes = await _clientes.FindAsync<Cliente>(cliente => true);
            return clientes?.ToEnumerable();
        }   
    }
}
