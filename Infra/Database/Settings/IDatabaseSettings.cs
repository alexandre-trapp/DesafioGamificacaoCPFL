using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DesafioGamificacaoCPFL.Infra.Database.Settings
{
    public interface IDatabaseSettings
    {
        string ClientesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string PontuacaoClienteCollectionName { get; set; }
    }
}
