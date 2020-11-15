using System;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Collections.Generic;
using DesafioGamificacaoCPFL.Models;
using DesafioGamificacaoCPFL.Infra.Database.Settings;

namespace DesafioGamificacaoCPFL.Infra.Database.Repositories
{
    public class PagamentoParcialRepository : IPagamentoParcialRepository
    {
        private readonly IMongoCollection<PagamentoParcial> _pagamentoParcial;

        public PagamentoParcialRepository(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _pagamentoParcial = database.GetCollection<PagamentoParcial>(settings.PagamentoParcialCollectionName);
        }

        public async Task CadastrarNovoPagamentoParcial(PagamentoParcial pagamentoParcial)
        {
            await _pagamentoParcial.InsertOneAsync(pagamentoParcial);
        }

        public async Task<IEnumerable<PagamentoParcial>> ConsultarPagamentosParciaisDoCliente(string clienteId)
        {
            var pagamentosParciais = await _pagamentoParcial.FindAsync<PagamentoParcial>(pagamento => pagamento.ClienteId == clienteId);
            return pagamentosParciais?.ToEnumerable();
        }
    }
}
