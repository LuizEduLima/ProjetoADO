using ADO.Business.Interfaces;
using ADO.Business.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADO.Web.Controllers
{
    public class CategoriaController : Controller
    {

        public readonly ICategoriaRepository _categoriaRepository;
        public readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaRepository categoriaRepository,
            ICategoriaService categoriaService)
        {
            _categoriaRepository = categoriaRepository;
            _categoriaService = categoriaService;
        }

        public async Task<ActionResult> Index()
        {
            var result = await _categoriaRepository.ObterTodos();
            return View(result);
        }

        public async Task<ActionResult> Details(int id)
        {
            if (id <= 0) return NotFound();

            var result = await _categoriaRepository.ObterPorId(id);

            if (result == null) return NotFound();

            return View(result);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Categoria categoria)
        {
            if (!ModelState.IsValid) return View(categoria);

            await _categoriaService.Adicionar(categoria);
            return RedirectToAction(nameof(Index));            
        }

        public async Task<ActionResult> Edit(int id)
        {
            var result = await _categoriaRepository.ObterPorId(id);
            if (result == null) return NotFound();
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Categoria categoria)
        {
            if (id != categoria.Id) return View();

            await _categoriaService.Atualizar(categoria);

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int id)
        {
            var result = await _categoriaRepository.ObterPorId(id);
            if (result == null) return NotFound();
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Categoria categoria)
        {

            await _categoriaService.Excluir(categoria.Id);
            return RedirectToAction(nameof(Index));

        }
    }
}
