using System.Collections.Generic;
using System.Threading.Tasks;
using DesafioGamificacaoCPFL.Models;

namespace DesafioGamificacaoCPFL.Infra.Database.Repositories
{
    public interface IPontuacaoClienteRepository
    {
        Task<Cliente> Get(string id);
        Task<IEnumerable<PontuacaoCliente>> GetAll();
        Task<string> Create(PontuacaoCliente pontuacaoCliente);
        Task<string> Delete(string id);
        Task<string> Update(PontuacaoCliente pontuacaoCliente);
    }
}
