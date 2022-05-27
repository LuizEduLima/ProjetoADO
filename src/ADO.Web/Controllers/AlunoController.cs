
using ADO.Business.Interfaces;
using ADO.Business.Models;
using ADO.Data.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADO.Web.Controllers
{
    public class AlunoController :MainController
    {
        private readonly IAlunoRepository _alunoRepository;

        public AlunoController(IAlunoRepository alunoRepository)
        {
            _alunoRepository = alunoRepository;
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
                AdicionarErroProcessamento("Ops! Erro não identificado");
                return CustomResponse(id);

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
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _alunoRepository.Adicionar(aluno);

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
        public async Task<ActionResult> Edit(int id,Aluno aluno)
        {
            if (id <= 0) return BadRequest();
            if (id != aluno.Id) return BadRequest();

            var result = await _alunoRepository.Atualizar(aluno);

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

            // var result = await _alunoRepository.ObterPorId(id);

            try
            {
                await _alunoRepository.Remover(id);

                return RedirectToAction(nameof(Index));
            }
            catch (DomainException)
            {

                throw new DomainException("");
            }
        
        }


    }
}
