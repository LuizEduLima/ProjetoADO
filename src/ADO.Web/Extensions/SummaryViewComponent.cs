using ADO.Business.Interfaces;
using ADO.Business.Notificacoes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ADO.Web.Extensions
{

    [ViewComponent(Name = "Summary")]
    public class SummaryViewComponent : ViewComponent
    {
     private readonly INotificador _notificador;

        public SummaryViewComponent(INotificador notificador)
        {
           _notificador = notificador;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var notificacoes = await Task.FromResult(_notificador.ObterNotificacoes());
            notificacoes.ForEach(m => ViewData.ModelState.AddModelError(string.Empty, m.Mensagem));
            return View();
        }


    }
}
