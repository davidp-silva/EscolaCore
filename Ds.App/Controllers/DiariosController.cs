using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ds.App.Data;
using Ds.App.ViewModels;
using AutoMapper;
using Ds.Business.Interfaces;
using Ds.Business.Models;

namespace Ds.App.Controllers
{
    public class DiariosController : BaseController
    {
        private readonly IDiarioRepository _diarioRepository;
        // private readonly IPessoaService _fornecedorService;
        private readonly IAlunoRepository _alunoRepository;
        private readonly IDisciplinaRepository _disciplinaRepository;
        private readonly IMapper _mapper;

        public DiariosController(IDiarioRepository diarioRepository, IMapper mapper,IAlunoRepository alunoRepository
            ,IDisciplinaRepository disciplinaRepository, INotificador notificador) :base (notificador)
        {
            _diarioRepository = diarioRepository;
            _mapper = mapper;
            _alunoRepository = alunoRepository;
            _disciplinaRepository = disciplinaRepository;
        }

        // GET: Diarios
        public async Task<IActionResult> Index()
        {
            
            return View( _mapper.Map<IEnumerable<DiarioViewModel>>( await _diarioRepository.ObterTodos()));
        }

        // GET: Diarios/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
         
            return View(_mapper.Map<DiarioViewModel>(await _diarioRepository.ObterPorId(id)));
        }

        // GET: Diarios/Create
        public async Task<IActionResult> CreateAsync()
        {

            var diarioViewModel = await PopulaAlunoeDisciplina(new DiarioViewModel());

            return View(diarioViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Data,DisciplinaId,AlunoId,Presenca")] DiarioViewModel diarioViewModel)
        {
            diarioViewModel = await PopulaAlunoeDisciplina(diarioViewModel);

            if (!ModelState.IsValid) return View(diarioViewModel);

            await _diarioRepository.Adicionar(_mapper.Map<Diario>(diarioViewModel));

            return RedirectToAction("Index");
        }

        // GET: Diarios/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diarioViewModel = await PopulaDiario(id);
            if (diarioViewModel == null)
            {
               return NotFound();
            }
            else
            {
                return View(diarioViewModel);
            }
            
           
        }

        // POST: Diarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Data,DisciplinaId,AlunoId,Presenca")] DiarioViewModel diarioViewModel)
        {
            if (id != diarioViewModel.Id)
            {
               return NotFound();
            }

            if (ModelState.IsValid)
            {
              
                    await _diarioRepository.Atualizar(_mapper.Map<Diario>(diarioViewModel));
            }
            else
            {
                return View(PopulaAlunoeDisciplina(diarioViewModel));
            }

            return View("Index");
        }

        // GET: Diarios/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diarioViewModel = await PopulaDiario(id);
            if (diarioViewModel == null)
            {
                return NotFound();
            }

            return View();
        }

        // POST: Diarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var diarioViewModel = await PopulaDiario(id);

            await _diarioRepository.Remover(id);

            return RedirectToAction(nameof(Index));
        }
        private async Task<DiarioViewModel> PopulaAlunoeDisciplina(DiarioViewModel diario)
        {
            diario.Alunos = _mapper.Map<IEnumerable<AlunoViewModel>>(await _alunoRepository.ObterTodos());
            diario.Disciplinas = _mapper.Map<IEnumerable<DisciplinaViewModel>>(await _disciplinaRepository.ObterTodos());

            return diario;
        }
        private async Task<DiarioViewModel> PopulaDiario(Guid id)
        {
            var diario = _mapper.Map<DiarioViewModel>(await _diarioRepository.ObterPorId(id));
            diario.Alunos = _mapper.Map<IEnumerable<AlunoViewModel>>(await _alunoRepository.ObterTodos());
            diario.Disciplinas = _mapper.Map<IEnumerable<DisciplinaViewModel>>(await _disciplinaRepository.ObterTodos());

            return diario;
        }
    }
}
