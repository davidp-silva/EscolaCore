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
    public class PeriodosController : BaseController
    {
        IPeriodoRepository _periodoRepository;
        IMapper _mapper;
        IPeriodoService _periodoService;

        public PeriodosController(IMapper mapper,IPeriodoRepository periodoRepository
            ,INotificador notificador,IPeriodoService periodoService) :base(notificador)
        {
            _mapper = mapper;
            _periodoRepository = periodoRepository;
            _periodoService = periodoService;
        }

        // GET: Periodos
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<PeriodoViewModel>>(await _periodoRepository.ObterTodos()));
        }

        // GET: Periodos/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<PeriodoViewModel>(await _periodoRepository.ObterPorId(id)));
        }

        // GET: Periodos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Periodos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomePeriodo")] PeriodoViewModel periodoViewModel)
        {
            if (!ModelState.IsValid) return View(periodoViewModel);

            var periodo = _mapper.Map<Periodo>(periodoViewModel);
            await _periodoService.Adicionar(periodo);

            if (!OperacaoValida()) return View(periodoViewModel);

            return RedirectToAction("Index");
        }

        // GET: Periodos/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<PeriodoViewModel>(await _periodoRepository.ObterPorId(id)));
        }

        // POST: Periodos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,NomePeriodo")] PeriodoViewModel periodoViewModel)
        {
            if (id != periodoViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(periodoViewModel);

            var periodo = _mapper.Map<Periodo>(periodoViewModel);

            await _periodoService.Atualizar(periodo);

            if (!OperacaoValida()) return View(periodoViewModel);

            return RedirectToAction("Index");
        }

        // GET: Periodos/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return View(_mapper.Map<PeriodoViewModel>(await _periodoRepository.ObterPorId(id)));
        }

        // POST: Periodos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var periodoViewModel = _mapper.Map<PeriodoViewModel>(await _periodoRepository.ObterPorId(id));

            if (periodoViewModel == null) return NotFound();

            await _periodoService.Remover(id);

            if (!OperacaoValida()) return View(periodoViewModel);

            return RedirectToAction("Index");
        }
    }
}
