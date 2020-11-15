using System.Threading.Tasks;
using System.Collections.Generic;
using DesafioGamificacaoCPFL.Models;

namespace DesafioGamificacaoCPFL.Infra.Database.Repositories
{
    public interface IPontuacaoClienteRepository
    {
        Task<PontuacaoCliente> Get(string clienteId);
        Task Create(PontuacaoCliente pontuacaoCliente);
        Task Delete(string clienteId);
        Task AtualizarPontosCliente(PontuacaoCliente pontuacaoCliente);
    }
}
