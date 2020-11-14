using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioGamificacaoCPFL.Infra.Database.Settings
{
    public interface IDatabaseSettings
    {
        const string ClientesCollectionName = "Clientes";

        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
