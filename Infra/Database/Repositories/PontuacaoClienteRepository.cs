using System;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Collections.Generic;
using DesafioGamificacaoCPFL.Models;
using DesafioGamificacaoCPFL.Infra.Database.Settings;

namespace DesafioGamificacaoCPFL.Infra.Database.Repositories
{
    public class PontuacaoClienteRepository : IPontuacaoClienteRepository
    {
        private readonly IMongoCollection<PontuacaoCliente> _pontuacaoCliente;

        public PontuacaoClienteRepository(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _pontuacaoCliente = database.GetCollection<PontuacaoCliente>(settings.PontuacaoClienteCollectionName);
        }

        public async Task Create(PontuacaoCliente pontuacaoCliente) =>
            await _pontuacaoCliente.InsertOneAsync(pontuacaoCliente);

        public async Task Delete(string clienteId) =>
            await _pontuacaoCliente.DeleteOneAsync(pontuacao => pontuacao.ClienteId == clienteId);

        public async Task<PontuacaoCliente> Get(string clienteId)
        {
            var pontuacaoCliente = await _pontuacaoCliente.FindAsync<PontuacaoCliente>(pontuacaoCliente => pontuacaoCliente.ClienteId == clienteId);

            if (pontuacaoCliente == null)
                throw new OperationCanceledException($"pontuacaoCliente não encontrado com o clienteId {clienteId}");

            return pontuacaoCliente.First();
        }

        public async Task<IEnumerable<PontuacaoCliente>> GetAll()
        {
            var pontuacaoClientes = await _pontuacaoCliente.FindAsync<PontuacaoCliente>(pontuacao => true);
            return pontuacaoClientes.ToEnumerable();
        }

        public async Task Update(PontuacaoCliente pontuacaoCliente) =>
            
            await _pontuacaoCliente.UpdateOneAsync(pontuacao => 
                pontuacao.ClienteId == pontuacaoCliente.ClienteId,
                Builders<PontuacaoCliente>.Update
                    .Set(pontuacao => pontuacao.QuantidadePontos, pontuacaoCliente.QuantidadePontos))

            .ConfigureAwait(false);
    }
}
