using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ADO.Business.Notificacoes;
using ADO.Business.Interfaces;

namespace ADO.Data.Repository
{
    public class BaseADO : IDisposable
    {
        private IConfiguration Configure { get; set; }
        public SqlConnection Connection { get; }

        private readonly INotificador _notificador;
        public BaseADO(IConfiguration configure, INotificador notificador)
        {
            Configure = configure;
            Connection = new SqlConnection(Configure.GetConnectionString("ADOWebContext"));
            _notificador = notificador;
        }


        public void Dispose()
        {
            Connection?.Dispose();
        }
    }
}
