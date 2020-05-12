﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ds.App.ViewModels
{
    public class NotaViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage ="O campo {0} é obrigatório")]
        [DisplayName("Nota")]
        public double ValorNota { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Disciplina")]
        public Guid DisciplinaId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Período")]
        public Guid PeriodoId { get; set; }

        public DisciplinaViewModel Disciplina { get; set; }

        public  AlunoViewModel Aluno { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Aluno")]
        public Guid AlunoId { get; set; }

        public PeriodoViewModel Periodo { get; set; }

 
        public IEnumerable<AlunoViewModel> Alunos { get; set; }

        public IEnumerable<DisciplinaViewModel> Disciplinas { get; set; }


        public IEnumerable <PeriodoViewModel> Periodos { get; set; }
    }
}
