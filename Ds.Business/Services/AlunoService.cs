using Ds.Business.Interfaces;
using Ds.Business.Models;
using Ds.Business.Models.Validations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ds.Business.Services
{
    public class AlunoService : BaseService, IAlunoService
    {
        private IAlunoRepository _alunoRepository;
        private IEnderecoRepository _enderecoRepository;
        
        public AlunoService(INotificador notificador,IAlunoRepository alunoRepository,IEnderecoRepository enderecoRepository) : base(notificador)
        {
            _alunoRepository = alunoRepository;
            _enderecoRepository = enderecoRepository;
        }

        public async Task Adicionar(Aluno aluno)
        {

            if (!ExecutarValidacao(new AlunoValidation(), aluno) || !ExecutarValidacao(new EnderecoValidation(), aluno.Endereco)) return;

            if(_alunoRepository.Buscar(a=>a.Documento==aluno.Documento).Result.Any())
            {
                Notificar("Já existe um Aluno com este documento");
                return;
            }

            await _alunoRepository.Adicionar(aluno);
        }

        public async Task Atualizar(Aluno aluno)
        {
            if (!ExecutarValidacao(new AlunoValidation(), aluno)) return;

            if (_alunoRepository.Buscar(f => f.Documento == aluno.Documento && f.Id != aluno.Id).Result.Any())
            {
                Notificar("Já existe um Aluno com este documento infomado.");
                return;
            }
            await _alunoRepository.Atualizar(aluno);
        }

      

        public async Task Remover(Guid id)
        {
            if (  _alunoRepository.ObterAlunoNotaCadastrada(id).Result.Nota.Any())
            {
                Notificar("O Aluno já possui notas cadastradas.");
                return;
            }

            await _alunoRepository.Remover(id);
        }

        public void Dispose()
        {
            _alunoRepository?.Dispose();
        }

        public async Task<Aluno> ObterAlunoEndereco(Guid id)
        {
            return await _alunoRepository.ObterAlunoEndereco(id);
        }


        public async Task AtualizarEndereco(Endereco endereco)
        {
            if (!ExecutarValidacao(new EnderecoValidation(), endereco)) return;

            await _enderecoRepository.Atualizar(endereco);
        }
    }
}
