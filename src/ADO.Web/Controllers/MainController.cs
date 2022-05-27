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
        private ICollection<string> Erros = new List<string>();

        protected ActionResult CustomResponse(object Result = null)
        {
            if (OperacaoValida())
            {
                return Ok(Result);
            }
            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            { 
            {"Mensagens",Erros.ToArray()}
            }));
            
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                AdicionarErroProcessamento(erro.ErrorMessage);
            }

            return CustomResponse();
        }


        protected bool OperacaoValida()
        {
            return !Erros.Any();
        }
        protected void AdicionarErroProcessamento( string erro)
        {
            Erros.Add(erro);
        }
        protected void LimparErrosProcessamentos()
        {
            Erros.Clear();
        }

    }
}
