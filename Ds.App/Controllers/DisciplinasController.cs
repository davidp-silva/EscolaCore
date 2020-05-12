using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ds.App.Data;
using Ds.App.ViewModels;
using Ds.Business.Interfaces;
using AutoMapper;
using Ds.Business.Models;

namespace Ds.App.Controllers
{
    public class DisciplinasController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IDisciplinaService _disciplinaService;

        public DisciplinasController(IMapper mapper, IDisciplinaService disciplinaService, INotificador notificador) :base(notificador)
        {
            
            _mapper = mapper;
            _disciplinaService = disciplinaService;
        }

        // GET: Disciplinas
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<DisciplinaViewModel>>(await _disciplinaService.ObterTodos()));
        }

        // GET: Disciplinas/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(await ObterDisciplina(id));
        }

        // GET: Disciplinas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Disciplinas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomeDisciplina")] DisciplinaViewModel disciplinaViewModel)
        {
            if (!ModelState.IsValid) return View(disciplinaViewModel);

            var aluno = _mapper.Map<Disciplina>(disciplinaViewModel);
            await _disciplinaService.Adicionar(aluno);

            if (!OperacaoValida()) return View(disciplinaViewModel);

            return RedirectToAction("Index");
        }

        // GET: Disciplinas/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(await ObterDisciplina(id));
        }

        // POST: Disciplinas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,NomeDisciplina")] DisciplinaViewModel disciplinaViewModel)
        {
            if (id != disciplinaViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(disciplinaViewModel);

            var disciplina = _mapper.Map<Disciplina>(disciplinaViewModel);
            await _disciplinaService.Atualizar(disciplina);

            if (!OperacaoValida()) return View(disciplinaViewModel);

            return RedirectToAction("Index");

        }

        // GET: Disciplinas/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var disciplinaViewModel = await ObterDisciplina(id);
            if (disciplinaViewModel == null)
            {
                return NotFound();
            }

            return View(disciplinaViewModel);
        }

        // POST: Disciplinas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var disciplinaViewModel = await ObterDisciplina(id);

            if (disciplinaViewModel == null) return NotFound();

            await _disciplinaService.Remover(id);

            if (!OperacaoValida()) return View(disciplinaViewModel);

            return RedirectToAction("Index");
        }

        private async Task<DisciplinaViewModel> ObterDisciplina (Guid id)
        {  
            return _mapper.Map<DisciplinaViewModel>(await _disciplinaService.ObterPorId(id));
        }

    }
}
