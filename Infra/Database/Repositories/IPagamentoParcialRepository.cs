using System.Threading.Tasks;
using DesafioGamificacaoCPFL.Models;

namespace DesafioGamificacaoCPFL.Infra.Database.Repositories
{
    public interface IPagamentoParcialRepository
    {
        Task<PagamentoParcial> ConsultarPagamentoParcialDoCliente(string clienteId);
        Task CadastrarNovoPagamentoParcial(PagamentoParcial pagamentoParcial);
    }
}
