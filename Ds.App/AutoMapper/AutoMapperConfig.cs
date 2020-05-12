using AutoMapper;
using Ds.App.ViewModels;
using Ds.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ds.App.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Pessoa, PessoaViewModel>().ReverseMap();
            CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
            CreateMap<Nota, NotaViewModel>().ReverseMap();
            CreateMap<Aluno, AlunoViewModel>().ReverseMap();
            CreateMap<Disciplina, DisciplinaViewModel>().ReverseMap();
            CreateMap<Periodo, PeriodoViewModel>().ReverseMap();
            
        }
    }
}
