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
using Ds.Business.Services;

namespace Ds.App.Controllers
{
    public class AlunosController : BaseController
    {

        IAlunoRepository _alunoRepository;
        IMapper _mapper;
        IAlunoService _alunoService;


        public AlunosController(IAlunoRepository alunoRepository, IMapper mapper,INotificador notificador,
              IAlunoService alunoService) : base(notificador)
        {
            _alunoRepository = alunoRepository;
            _alunoService = alunoService;
            _mapper = mapper;

        }

        // GET: Alunos
        public async Task<IActionResult> Index()
        {
            return View( _mapper.Map<IEnumerable<AlunoViewModel>>(await _alunoRepository.ObterTodos()));
        }

        // GET: Alunos/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<AlunoViewModel>(await _alunoRepository.ObterPorId(id)));
        }

        // GET: Alunos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Alunos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomeCompleto,Documento,Ativo,DataNascimento,Telefone")] AlunoViewModel alunoViewModel)
        {
            if (!ModelState.IsValid) return View(alunoViewModel);

            var aluno = _mapper.Map<Aluno>(alunoViewModel);
            await _alunoService.Adicionar(aluno);

            if (!OperacaoValida()) return View(alunoViewModel);

            return RedirectToAction("Index");
        }

        // GET: Alunos/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<AlunoViewModel>(await _alunoRepository.ObterPorId(id)));
        }

        // POST: Alunos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,NomeCompleto,Documento,Ativo,DataNascimento,Telefone")] AlunoViewModel alunoViewModel)
        {
            if (id != alunoViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(alunoViewModel);

            var aluno = _mapper.Map<Aluno>(alunoViewModel);
            await _alunoService.Atualizar(aluno);

            if (!OperacaoValida()) return View(alunoViewModel);

            return RedirectToAction("Index");
        }

        // GET: Alunos/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<AlunoViewModel>(await _alunoRepository.ObterPorId(id)));
        }

        // POST: Alunos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var alunoViewModel = _mapper.Map<AlunoViewModel>(await _alunoRepository.ObterPorId(id));

            if (alunoViewModel == null) return NotFound();

            await _alunoService.Remover(id);

            if (!OperacaoValida()) return View(alunoViewModel);

            return RedirectToAction("Index");
        }
    }
}
