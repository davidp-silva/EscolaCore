using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ds.App.ViewModels
{
    public class AlunosResponsavelViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid AlunoId { get; set; }

        public AlunoViewModel Alunos { get; set; }

        public Guid ResponsavelId { get; set; }

        public ResponsavelViewModel Responsaveis { get; set; }
    }
}
