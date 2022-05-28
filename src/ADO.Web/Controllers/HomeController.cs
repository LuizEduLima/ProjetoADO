using ADO.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using Microsoft.Extensions.Configuration;
using ADO.Business.Interfaces;

namespace ADO.Web.Controllers
{
    public class HomeController : Controller
    {
       

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {

            return View();
        }

        
        [Route("erro/{id:int}")]
        public IActionResult Error(int id)
        {
            var erroViewModel = new ErrorViewModel();

            switch (id)
            {

                case 500:
                    erroViewModel.Mensagem = "Ocorreu um Erro :( ! Tente novamente mais tarde!!!";
                    erroViewModel.Titulo = "Ocorreu um erro!";
                    erroViewModel.ErroCode = id;
                    break;

                case 404:
                    erroViewModel.Mensagem = "A página que está procurando não existe! <br/>Em caso de dúvidas entre em contato com o nosso suporte!";
                    erroViewModel.Titulo = "Ops! Página não encontrada";
                    erroViewModel.ErroCode = id;
                    break;

                case 403:
                    erroViewModel.Mensagem = "Você não tem permissão para fazer isto.";
                    erroViewModel.Titulo = "Acesso Negado!";
                    erroViewModel.ErroCode = id;
                    break;

                default:
                    return StatusCode(500);
            }

            return View("Error", erroViewModel);

        }



    }


}
