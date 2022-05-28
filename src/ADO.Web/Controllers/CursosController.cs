using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ADO.Business.Models;
using ADO.Data.Data;
using ADO.Business.Interfaces;

namespace ADO.Web.Controllers
{
    public class CursosController : MainController
    {
        private readonly ICursoRepository _cursoRepository;
        private readonly ICursoService _cursoService;


        public CursosController(ICursoRepository cursoRepository, 
                                INotificador notificacao, 
                                ICursoService cursoService)
            : base(notificacao)
        {
            _cursoRepository = cursoRepository;
            _cursoService = cursoService;
        }


        public async Task<IActionResult> Index()
        {
            var result = await _cursoRepository.ObterTodos();
            return View(result);
        }

        public async Task<IActionResult> Details(int id)
        {
            var curso = await _cursoRepository.ObterPorId(id);          

            return View(curso);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Curso curso)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _cursoService.Adicionar(curso);
            if (result == null)
            {
                AdicionarErroNotificacao("Erro ao adicionar Curso");
                return View(curso);
            }

            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> Edit(int id)
        {
            var curso = await _cursoRepository.ObterPorId(id);
            if (curso == null)
            {
                return BadRequest();
            }
            return View(curso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Curso curso)
        {
            if (id != curso.Id)
            {
                AdicionarErroNotificacao("Os Ids não conferem!");
                return View(curso);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _cursoService.Atualizar(curso);

            return RedirectToAction(nameof(Index));

        }

        // GET: Cursos/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var curso = await _cursoRepository.ObterPorId(id);
            if (curso == null)
            {
                return BadRequest(curso);
            }

            return View(curso);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var curso = await _cursoRepository.ObterPorId(id);
            if (curso == null)
            {
                AdicionarErroNotificacao($"O Curso com o Registro: {id} não foi encontrado");
                return BadRequest();
            }

            await _cursoService.Excluir(id);


            return RedirectToAction(nameof(Index));
        }


    }
}
