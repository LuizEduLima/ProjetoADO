using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ADO.Business.Models;
using ADO.Business.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ADO.Web.Controllers
{
    public class CursosController : MainController
    {
        private readonly ICursoRepository _cursoRepository;
        private readonly ICursoService _cursoService;
        private readonly ICategoriaRepository _CategoriaRepository;
        private readonly IAlunoRepository _alunoRepository;

        public CursosController(ICursoRepository cursoRepository,
                                INotificador notificacao,
                                ICursoService cursoService,
                                ICategoriaRepository categoriaRepository,
                                IAlunoRepository alunoRepository)
            : base(notificacao)
        {
            _cursoRepository = cursoRepository;
            _cursoService = cursoService;
            _CategoriaRepository = categoriaRepository;
            _alunoRepository = alunoRepository;
        }


        public async Task<IActionResult> Index()
        {
            var result = await _cursoRepository.ObterTodos();
            return View(result);
        }
        public async Task<IActionResult> ObterAlunosPorCursos()
        {
            var result = await _cursoRepository.ObterAlunosPorCursos();
            return View(result);
        }

        public async Task<IActionResult> Details(int id)
        {
            var curso = await _cursoRepository.ObterPorId(id);

            return View(curso);
        }


        public async Task<IActionResult> Create()
        {
            var curso = new Curso
            {
                Categorias = await ObterCategorias()
            };

            return View(curso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Curso curso)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _cursoService.Adicionar(curso);
            if (!OperacaoValida()) return View(curso);

            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> Edit(int id)
        {
            var curso = await _cursoRepository.ObterPorId(id);
            if (curso == null)
            {
                return BadRequest();
            }
            curso.Categorias = await ObterCategorias();
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

            if (!OperacaoValida()) return View(curso);

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

            if (!OperacaoValida()) return View(curso);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> MatricularAlunoCurso()
        {

            var alunocurso = await CarregarAlunosECursos();


            return View(alunocurso);
        }

        [HttpPost()]
        public async Task<IActionResult> MatricularAlunoCurso(AlunoCurso alunoCurso)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _cursoRepository.MatricularAlunoCurso(alunoCurso.Aluno_id, alunoCurso.Curso_Id);

            if (!OperacaoValida())
            {
                alunoCurso = await CarregarAlunosECursos();
                return View(alunoCurso);
            }

            return RedirectToAction("ObterAlunosPorCursos");
        }
        private async Task<IEnumerable<Categoria>> ObterCategorias()
        {
            return await _CategoriaRepository.ObterTodos();
        }

        private async Task<AlunoCurso> CarregarAlunosECursos()
        {
            var Alunos = await _alunoRepository.ObterTodos();
            var Cursos = await _cursoRepository.ObterTodos();
            var alunocurso = new AlunoCurso
            {
                alunos = Alunos.ToList(),
                cursos = Cursos.ToList()
            };
            return alunocurso;
        }
    }
}
