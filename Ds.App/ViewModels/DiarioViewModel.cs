using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ds.App.ViewModels
{
    public class DiarioViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime Data { get; set; }

        public Guid DisciplinaId { get; set; }
        public DisciplinaViewModel Disciplina { get; set; }
  
        public Guid AlunoId { get; set; }

        public AlunoViewModel Aluno { get; set; }

        public bool Presenca { get; set; }

        [NotMapped]
        public IEnumerable<AlunoViewModel> Alunos { get; set; }
        [NotMapped]
        public IEnumerable<DisciplinaViewModel> Disciplinas { get; set; }
    }
}
