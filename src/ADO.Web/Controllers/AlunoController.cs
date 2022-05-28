
using ADO.Business.Interfaces;
using ADO.Business.Models;
using ADO.Business.Services;
using ADO.Data.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADO.Web.Controllers
{
    public class AlunoController : MainController
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly AlunoService _alunoService;
        public AlunoController(IAlunoRepository alunoRepository,
                               INotificador notificador,
                               AlunoService alunoService) : base(notificador)
        {
            _alunoRepository = alunoRepository;
            _alunoService = alunoService;
        }

        [HttpGet("")]
        [HttpGet("lista-alunos")]
        public async Task<ActionResult> Index()
        {
            var result = await _alunoRepository.ObterTodos();
            return View(result);
        }

        [HttpGet("detalhes-aluno/{id:int}")]
        public async Task<ActionResult> Details(int id)
        {
            if (id <= 0)
            {
                AdicionarErroNotificacao("O Registro não foi informado!");
                return BadRequest();
            }
            var result = await _alunoRepository.ObterPorId(id);
            return View(result);

        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Aluno aluno)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _alunoService.Adicionar(aluno);

            if (!OperacaoValida()) return View(aluno);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("editar-aluno/{id:int}")]
        public async Task<ActionResult> Edit(int id)
        {
            var result = await _alunoRepository.ObterPorId(id);

            if (result == null) return NotFound();

            return View(result);
        }

        [HttpPost("editar-aluno/{id:int}")]
        public async Task<ActionResult> Edit(int id, Aluno aluno)
        {

            if (id != aluno.Id) return BadRequest();

            if (!ModelState.IsValid) return BadRequest();


            var result = await _alunoService.Atualizar(aluno);

            return RedirectToAction("Details", new { result.Id });
        }

        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();

            var result = await _alunoRepository.ObterPorId(id);

            return View(result);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (id <= 0) return BadRequest();
            await _alunoService.Excluir(id);

            return RedirectToAction(nameof(Index));


        }


    }
}
