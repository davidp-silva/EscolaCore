using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ds.App.ViewModels;
using Ds.Business.Interfaces;
using AutoMapper;
using Ds.Business.Models;

namespace Ds.App.Controllers
{
    public class NotasController : BaseController
    {
        private readonly INotaRepository _notaRepository;
        private readonly IMapper _mapper;
        private readonly IAlunoRepository _alunoRepository;
        private readonly IDisciplinaRepository _disciplinaRepository;
        private readonly IPeriodoRepository _periodoRepository;
        private readonly INotaService _notaService;


        public NotasController(INotaRepository notaRepository, IMapper mapper,
            IAlunoRepository alunoRepository,
            IDisciplinaRepository disciplinaRepository,
            IPeriodoRepository periodoRepository,
            INotaService notaService, INotificador notificador) : base(notificador)
        {
            _notaRepository = notaRepository;
            _mapper = mapper;
            _alunoRepository = alunoRepository;
            _disciplinaRepository = disciplinaRepository;
            _periodoRepository = periodoRepository;
            _notaService = notaService;

        }

        // GET: Notas
        public async Task<IActionResult> Index()
        {

            var todos = await _notaRepository.ObterTodos();
            var viewModel = _mapper.Map<IEnumerable<NotaViewModel>>(await _notaRepository.ObterTodos());
            return View(viewModel);
        }

        // GET: Notas/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<NotaViewModel>(await _notaRepository.ObterPorId(id)));
        }

        // GET: Notas/Create
        public async Task<IActionResult> CreateAsync()
        {
            return View(await BuscarDadosCombos(new NotaViewModel()));
        }

        // POST: Notas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NotaViewModel notaViewModel)
        {
            notaViewModel = await BuscarDadosCombos(notaViewModel);

            if (!ModelState.IsValid) return View(notaViewModel);

            var nota = _mapper.Map<Nota>(notaViewModel);

            await _notaService.Adicionar(nota);

            if (!OperacaoValida()) return View(notaViewModel);

            return RedirectToAction("Index");
        }

        // GET: Notas/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notaViewModel = _mapper.Map<NotaViewModel>(await _notaRepository.ObterPorId(id));
            
            if (notaViewModel == null)
            {
                return NotFound();
            }

            return View(await BuscarDadosCombos(notaViewModel));
        }

        // POST: Notas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ValorNota,DisciplinaId,PeriodoId,AlunoId")] NotaViewModel notaViewModel)
        {
            var notaOriginal = await BuscarDadosCombos(new NotaViewModel());
            notaViewModel.Alunos = notaOriginal.Alunos;
            notaViewModel.Disciplinas = notaOriginal.Disciplinas;
            notaViewModel.Periodos = notaOriginal.Periodos;

            if (id != notaViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(notaViewModel);

            var nota = _mapper.Map<Nota>(notaViewModel);

            await _notaService.Atualizar(nota);

            if (!OperacaoValida()) return View(notaViewModel);

            return RedirectToAction("Index");
        }
        

        // GET: Notas/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var notaViewModel = _mapper.Map<NotaViewModel>(await _notaRepository.ObterPorId(id));
            return View(await BuscarDadosCombos(notaViewModel));
        }

        // POST: Notas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var notaViewModel = _mapper.Map<NotaViewModel>(await _notaRepository.ObterPorId(id));

            if (notaViewModel == null) return NotFound();

            await _notaService.Remover(id);

            if (!OperacaoValida()) return View(notaViewModel);

            return RedirectToAction("Index");
        }

        private async Task<NotaViewModel> BuscarDadosCombos(NotaViewModel notaViewModel)
        {
            notaViewModel.Alunos = _mapper.Map<IEnumerable<AlunoViewModel>>(await _alunoRepository.ObterTodos());
            notaViewModel.Disciplinas = _mapper.Map<IEnumerable<DisciplinaViewModel>>(await _disciplinaRepository.ObterTodos());
            notaViewModel.Periodos = _mapper.Map<IEnumerable<PeriodoViewModel>>(await _periodoRepository.ObterTodos());

            return notaViewModel;
        }

    }
}
