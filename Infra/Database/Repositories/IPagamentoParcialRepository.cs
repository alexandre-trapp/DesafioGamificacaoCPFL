using System.Threading.Tasks;
using System.Collections.Generic;
using DesafioGamificacaoCPFL.Models;

namespace DesafioGamificacaoCPFL.Infra.Database.Repositories
{
    public interface IPagamentoParcialRepository
    {
        Task CadastrarNovoPagamentoParcial(PagamentoParcial pagamentoParcial);
        Task<IEnumerable<PagamentoParcial>> ConsultarPagamentosParciaisDoCliente(string clienteId);
    }
}
