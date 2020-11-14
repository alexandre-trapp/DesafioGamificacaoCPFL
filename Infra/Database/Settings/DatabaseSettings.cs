﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DesafioGamificacaoCPFL.Infra.Database.Settings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}