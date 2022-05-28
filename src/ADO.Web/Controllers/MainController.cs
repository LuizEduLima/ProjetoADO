using ADO.Business.Interfaces;
using ADO.Business.Notificacoes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADO.Web.Controllers
{
    public abstract class MainController : Controller
    {

        private readonly INotificador _notificacao;

        protected MainController(INotificador notificacao)
        {
            _notificacao = notificacao;
        }
        

        protected bool OperacaoValida()
        {
            return !_notificacao.TemNotificacao();
        }
        protected void AdicionarErroNotificacao(string mensagem)
        {
            _notificacao.Handle(new Notificacao(mensagem));
        }
      


    }
}
