using ADO.Business.Interfaces;
using ADO.Business.Notificacoes;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace ADO.Business.Services
{
    public abstract class BaseService
    {
        private IConfiguration Configure { get; set; }
        protected SqlConnection Connection { get; }

        protected readonly INotificador _notificador;       

        protected BaseService(IConfiguration configure, INotificador notificador)
        {
            Configure = configure;
            Connection = new SqlConnection(Configure.GetConnectionString("ADOWebContext"));
            _notificador = notificador;
        }        

        public void Dispose()
        {
            Connection?.Dispose();
        }

        protected void AdicionarNotificacoes(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }
    }
}
