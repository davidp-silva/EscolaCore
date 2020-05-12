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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
        public async Task<IActionResult> Create(AlunoViewModel alunoViewModel)
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

            return View(_mapper.Map<AlunoViewModel>(await _alunoRepository.ObterAlunoEndereco(id)));
        }

        // POST: Alunos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,AlunoViewModel alunoViewModel)
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

        [AllowAnonymous]
        [Route("obter-endereco-aluno/{id:guid}")]
        public async Task<IActionResult> ObterEndereco(Guid id)
        {
            var fornecedor = await ObterEnderecoAluno(id);

            if (fornecedor == null)
            {
                return NotFound();
            }

            return PartialView("_DetalhesEndereco", fornecedor);
        }

        //[ClaimsAuthorize("Fornecedor", "Editar")]
        [Route("atualizar-endereco-aluno/{id:guid}")]
        public async Task<IActionResult> AtualizarEndereco(Guid id)
        {
            var aluno = await ObterEnderecoAluno(id);

            if (aluno == null)
            {
                return NotFound();
            }

            return PartialView("_AtualizarEndereco", new AlunoViewModel { Endereco = aluno.Endereco });
        }

       // [ClaimsAuthorize("Fornecedor", "Editar")]
        [Route("atualizar-endereco-aluno/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> AtualizarEndereco(AlunoViewModel alunoViewModel)
        {
            ModelState.Remove<AlunoViewModel>(c => c.DataNascimento);
            ModelState.Remove<AlunoViewModel>(c => c.Documento);
            ModelState.Remove<AlunoViewModel>(c => c.NomeCompleto);
            ModelState.Remove<AlunoViewModel>(c => c.Telefone);

            if (!ModelState.IsValid) return PartialView("_AtualizarEndereco", alunoViewModel);

            await _alunoService.AtualizarEndereco(_mapper.Map<Endereco>(alunoViewModel.Endereco));

            if (!OperacaoValida()) return PartialView("_AtualizarEndereco", alunoViewModel);

            var url = Url.Action("ObterEndereco", "Alunos", new { id = alunoViewModel.Endereco.PessoaId });
            return Json(new { success = true, url });
        }

        private async Task<AlunoViewModel> ObterEnderecoAluno(Guid id)
        {
            return _mapper.Map<AlunoViewModel>(await _alunoService.ObterAlunoEndereco(id));
        }
    }
}
