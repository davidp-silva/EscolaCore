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
using Ds.Business.Models;
using AutoMapper;

namespace Ds.App.Controllers
{
    public class PessoasController : BaseController
    {
        private readonly IPessoaRepository _pessoaRepository;
       // private readonly IPessoaService _fornecedorService;
        private readonly IMapper _mapper;


        public PessoasController( IPessoaRepository pessoaRepository, IMapper mapper, INotificador notificador) :base(notificador)
        {
            _pessoaRepository = pessoaRepository;
            _mapper = mapper;
        }

        // GET: Pessoas
        public async Task<IActionResult> Index()
        {
            return View( _mapper.Map<IEnumerable<PessoaViewModel>>(await _pessoaRepository.ObterTodos()));
        }

        // GET: Pessoas/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<PessoaViewModel>(await _pessoaRepository.ObterPorId(id)));
        }

        // GET: Pessoas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pessoas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomeCompleto,Documento,Ativo,DataNascimento,Telefone")] PessoaViewModel pessoaViewModel)
        {
            if (ModelState.IsValid)
            {

                await _pessoaRepository.Adicionar(_mapper.Map<Pessoa>(pessoaViewModel));
                return RedirectToAction(nameof(Index));
            }
            return View(pessoaViewModel);
        }

        // GET: Pessoas/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoaViewModel = _mapper.Map<PessoaViewModel>(await _pessoaRepository.ObterPorId(id));
          
            return View(pessoaViewModel);
        }

        // POST: Pessoas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,NomeCompleto,Documento,Ativo,DataNascimento,Telefone")] PessoaViewModel pessoaViewModel)
        {
            if (id != pessoaViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _pessoaRepository.Atualizar(_mapper.Map<Pessoa>(pessoaViewModel));
            }
            else
            { 
                return RedirectToAction(nameof(Index));
            }

            return View(pessoaViewModel);
        }

        // GET: Pessoas/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoaViewModel  = _mapper.Map<PessoaViewModel>(await _pessoaRepository.ObterPorId(id)); 
                
            if (pessoaViewModel == null)
            {
                return NotFound();
            }

            return View(pessoaViewModel);
        }

        // POST: Pessoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var pessoaViewModel = _mapper.Map<PessoaViewModel>(await _pessoaRepository.ObterPorId(id));

            if(pessoaViewModel!= null)
            await _pessoaRepository.Remover(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
