using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ADO.Business.Models;
using ADO.Web.Data;
using ADO.Business.Interfaces;

namespace ADO.Web.Controllers
{
    public class CursosController : MainController
    {
        private readonly ICursoRepository _cursoRepository;

        public CursosController(ICursoRepository cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }


        public async Task<IActionResult> Index()
        {
            var result = await _cursoRepository.ObterTodos();
            return View(result);
        }

        public async Task<IActionResult> Details(int id)
        {

            var curso = await _cursoRepository.ObterPorId(id);

            if (curso == null)
            {
                return CustomResponse(curso);
            }

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
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _cursoRepository.Adicionar(curso);
            if (result == 0)
            {
                AdicionarErroProcessamento("Erro ao adicionar Curso");
                return CustomResponse();
            }

            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> Edit(int id)
        {

            var curso = await _cursoRepository.ObterPorId(id);
            if (curso == null)
            {
                return CustomResponse(curso);
            }
            return View(curso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Curso curso)
        {
            if (id != curso.Id)
            {
                AdicionarErroProcessamento("Os Ids não conferem!");
                return CustomResponse();
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _cursoRepository.Atualizar(curso);

            return RedirectToAction(nameof(Index));

        }

        // GET: Cursos/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var curso = await _cursoRepository.ObterPorId(id);
            if (curso == null)
            {
                return CustomResponse(curso);
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
                AdicionarErroProcessamento($"O Curso com o Registro: {id} não foi encontrado");
                return CustomResponse();
            }

            var result = await _cursoRepository.Remover(id);

            if (result == 0)
            {
                AdicionarErroProcessamento("Error ao Excluir o curso!");
                return CustomResponse();
            }
            return RedirectToAction(nameof(Index));
        }

      
    }
}
