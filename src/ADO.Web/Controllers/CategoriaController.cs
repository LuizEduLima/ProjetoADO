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

        public CategoriaController(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<ActionResult> Index()
        {
            var result= await _categoriaRepository.ObterTodos();
            return View(result);
        }

        // GET: CategoriaController/Details/5
        public ActionResult Details(int id)
        {
            if (id <= 0) return NotFound();

            var result = _categoriaRepository.ObterPorId(id);

            if (result == null) return NotFound();
            return View(result);
        }

        // GET: CategoriaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Categoria categoria)
        {
            if (!ModelState.IsValid) return View(categoria);
            try
            {
                await _categoriaRepository.Adicionar(categoria);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoriaController/Edit/5
        public ActionResult Edit(int id)
        {
            var result = _categoriaRepository.ObterPorId(id);
            if (result == null) return NotFound();
            return View(result);
        }

        // POST: CategoriaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Categoria categoria)
        {
            if(id!=categoria.Id) return View();
            try
            {
                await _categoriaRepository.Atualizar(categoria);
                return RedirectToAction(nameof(Details));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoriaController/Delete/5
        public ActionResult Delete(int id)
        {
            var result = _categoriaRepository.ObterPorId(id);
            if (result == null) return NotFound();
            return View(result);
        }

        // POST: CategoriaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Categoria categoria)
        {
            try
            {
                await _categoriaRepository.Remover(categoria.Id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
