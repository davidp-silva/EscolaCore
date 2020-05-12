using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ds.App.ViewModels
{
    public class AlunoViewModel :PessoaViewModel
    {

        public IEnumerable<AlunosResponsavelViewModel> AlunoResponsaveis { get; set; }

        public DiarioViewModel Diario { get; set; }

        [IgnoreMap]
        public IEnumerable<NotaViewModel> Nota { get; set; }

        public IEnumerable<TurmaAlunoViewModel> TurmaAlunos { get; set; }
    }
}
