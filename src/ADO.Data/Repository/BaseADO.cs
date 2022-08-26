using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ADO.Business.Notificacoes;
using ADO.Business.Interfaces;
using ADO.Data.Data;

namespace ADO.Data.Repository
{
    public abstract class BaseADO : IDisposable
    {
        private IConfiguration Configure { get; set; }
        public SqlConnection Connection { get; }
        public ADOWebContext _context;

        private readonly INotificador _notificador;
        protected BaseADO(IConfiguration configure, INotificador notificador, ADOWebContext context)
        {
            Configure = configure;
            Connection = new SqlConnection(Configure.GetConnectionString("ADOWebContext"));
            _notificador = notificador;
            _context = context;
        }


        public void Dispose()
        {
            Connection?.Dispose();
        }
        protected void AdicionarErroProcessamento(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }

    }
}
